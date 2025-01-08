using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Text;
using Utility.Extentions;
using Utility.Encryption;
using DAL.Models.Response;
using DAL.Domain;
using static Utility.Enums.SystemEnum;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Utility;

namespace BLL.Services.Communication.Nagad;

public interface INagadComunicationService
{
    Task<BankComAddmoneyResponse> PaymentInitiate(string transactionId, decimal transactionAmount);
    Task<BankAddmoneyInqueryResponse> PaymentInquery(string trasactionId, string referenceId);
}
public class NagadCommunicationService : INagadComunicationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private string _merchantId;
    private string _accountNumber;
    private string _ipV4;
    private string _transactionAmount;
    private string _baseUrl;
    private string _nagadGatewayPublicKey;
    private string _merchantPrivateKey;
    private string _merchantCallbackURL;
    public NagadCommunicationService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _configuration = configuration;

        #region Populate Data
        _merchantId = _configuration.GetSection("BankCommunication:Nagad:MerchantId").Value;
        _accountNumber = _configuration.GetSection("BankCommunication:Nagad:AccountNumber").Value;
        _ipV4 = _configuration.GetSection("BankCommunication:Nagad:IPV4").Value;
        _baseUrl = _configuration.GetSection("BankCommunication:Nagad:BaseUrl").Value;
      
        _nagadGatewayPublicKey = $"-----BEGIN PUBLIC KEY-----\r\n{_configuration.GetSection("BankCommunication:Nagad:NagadGatewayPublicKey").Value}\r\n-----END PUBLIC KEY-----"; // Replace with Nagad Gateway Public Key
        _merchantPrivateKey = $"-----BEGIN PRIVATE KEY-----\r\n{_configuration.GetSection("BankCommunication:Nagad:MerchantPrivateKey").Value}\r\n-----END PRIVATE KEY-----"; // Replace with Merchant Private Key

        #endregion
    }
    public async Task<BankComAddmoneyResponse> PaymentInitiate(string transactionId, decimal transactionAmount)
    {
        _merchantCallbackURL = $"{_configuration.GetSection("AppConfig:CoreApiGatewayUrl").Value}/api/v1/nagad-confirm-return";
        var bankResponse = new BankComAddmoneyResponse();
        var log = new ApplicationLog();
        log.BankCode = "nagad";
        try
        {
            log.TransactionId = transactionId;
            log.Service = "NagadPaymentInitiate";
            log.Status = TxnStatus.Processing.ToString();

            _transactionAmount = transactionAmount.ToTruncateDecimal().ToString();
            var orderid = transactionId;// Utility.Utils.GenerateTransactionId();
            var checkOut = new Nagad_CheckOut();
            checkOut.accountNumber = _accountNumber;
            checkOut.dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _sensitiveData = new Nagad_SensitiveDataModel();
            _sensitiveData.merchantId = _merchantId;// "683002007104225";
            _sensitiveData.datetime = checkOut.dateTime;
            _sensitiveData.orderId = orderid;
            _sensitiveData.challenge = EncryptionNagad.GenerateRandomHexString(20);
            string plainSensitiveData = JsonConvert.SerializeObject(_sensitiveData);
            //Log.Information("Nagad Init Api Raw Request: {Request}", plainSensitiveData);
            // Step 1: Encrypt sensitive data
            string encryptedSensitiveData = EncryptionNagad.EncryptData(plainSensitiveData, _nagadGatewayPublicKey);
            // Step 2: Generate digital signature
            string signature = EncryptionNagad.SignData(plainSensitiveData, _merchantPrivateKey);
            checkOut.sensitiveData = encryptedSensitiveData; //  generate
            checkOut.signature = signature; // generate

            // ##################################  Payment init  ######################################
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/dfs/check-out/initialize/{_merchantId}/{orderid}");
            request.Headers.Add("X-KM-Api-Version", "v-0.2.0");
            request.Headers.Add("X-KM-Client-Type", "MOBILE_APP");
            request.Headers.Add("X-KM-IP-V4", _ipV4);
            request.Content = new StringContent(JsonConvert.SerializeObject(checkOut), Encoding.UTF8, "application/json");
            log.Request = await request.ToHttpLog();
            await _unitOfWork.ApplicationLogRepository.Add(log);
            await _unitOfWork.CompleteAsync();
            Log.Information("======== 1. Nagad Init Api Request: {Request}, {Request_PlainSensitiveData}", log.Request, plainSensitiveData);
            var response = await client.SendAsync(request);
            var log_response = await response.ToHttpLog();
            log.Response = log_response;
            _unitOfWork.ApplicationLogRepository.Update(log);
            await _unitOfWork.CompleteAsync();
            Log.Information("========= 1. Nagad Init Api Response: {Response}", log_response);
            var responseData = await response.Content.ReadAsAsync<Nagad_CheckoutApiResponse>();
            var responseSensitive = EncryptionNagad.DecryptData(responseData.sensitiveData, _merchantPrivateKey);
            var sensitiveObj = JsonConvert.DeserializeObject<Nagad_CheckoutApiSensitiveResponse>(responseSensitive);

            if (!EncryptionNagad.VerifySignature(responseSensitive, responseData.signature, _nagadGatewayPublicKey))
            {
                return new BankComAddmoneyResponse { StatusCode = 100, Message = "Invalid Signature." };
            }
            //######################  Payment Complete  ################################
            var log_com = new ApplicationLog();
            log_com.TransactionId = transactionId;
            log_com.Service = "NagadPaymentComplete";
            log_com.Status = TxnStatus.Processing.ToString();

            var client_com = new HttpClient();
            var checkOut_com = new Nagad_CheckOutComplete();
            checkOut_com.merchantCallbackURL = _merchantCallbackURL;
            var sensitiveCom = new Nagad_CheckOutCompleteSensitive();
            sensitiveCom.merchantId = _merchantId;
            sensitiveCom.orderId = orderid;
            sensitiveCom.amount = _transactionAmount;
            sensitiveCom.currencyCode = "050";
            sensitiveCom.challenge = sensitiveObj.challenge;
            var senCom = JsonConvert.SerializeObject(sensitiveCom);
            checkOut_com.sensitiveData = EncryptionNagad.EncryptData(senCom, _nagadGatewayPublicKey);
            checkOut_com.signature = EncryptionNagad.SignData(senCom, _merchantPrivateKey);

            var request_com = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/dfs/check-out/complete/{sensitiveObj.paymentReferenceId}");
            request_com.Headers.Add("X-KM-Api-Version", "v-0.2.0");
            request_com.Headers.Add("X-KM-Client-Type", "MOBILE_APP");
            request_com.Headers.Add("X-KM-IP-V4", _ipV4);
            var content_com = new StringContent(JsonConvert.SerializeObject(checkOut_com), Encoding.UTF8, "application/json");
            request_com.Content = content_com;
            var log_request_com = await request_com.ToHttpLog();
            log_com.Request = log_request_com;
            await _unitOfWork.ApplicationLogRepository.Add(log_com);
            await _unitOfWork.CompleteAsync();
            Log.Information("============== 2. Nagad Payment Complete Api Request: {Request}, {Request_SensitiveData}", log_request_com, senCom);
            var response_com = await client_com.SendAsync(request_com);
            var log_response_com = await response_com.ToHttpLog();
            log_com.Response = log_response_com;
            _unitOfWork.ApplicationLogRepository.Update(log_com);
            await _unitOfWork.CompleteAsync();
            Log.Information("============== 2. Nagad Payment Complete Api Response: {Response}", log_response_com);
            //response.EnsureSuccessStatusCode();
            if (response_com.StatusCode != System.Net.HttpStatusCode.OK)
            {
                log.Status = TxnStatus.Fail.ToString();
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();
                return new BankComAddmoneyResponse { StatusCode = 100, Message = "Nagad Comunication failed" };
            }
            var responseData_com = await response_com.Content.ReadAsAsync<Nagad_CaheckOutCompleteApiResponse>();

            log.Status = TxnStatus.Success.ToString();
            _unitOfWork.ApplicationLogRepository.Update(log);
            await _unitOfWork.CompleteAsync();

            bankResponse.ReferenceId = sensitiveObj.paymentReferenceId;
            bankResponse.StatusCode = 200;
            bankResponse.IsRedirect = true;
            bankResponse.RedirectUrl = responseData_com.callBackUrl;
            bankResponse.Message = "Redirect Url";
            return bankResponse;

        }
        catch (Exception ex)
        {

            Log.Error(ex, "NagadPaymentInitiate_Exception");
            if (log.Id > 0)
            {
                log.Response = ex.Message;
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                log.Response = ex.Message;
                await _unitOfWork.ApplicationLogRepository.Add(log);
                await _unitOfWork.CompleteAsync();
            }
            bankResponse.StatusCode = 300;
            bankResponse.Message = ex.Message;
            return bankResponse;
        }
    }

    public async Task<BankAddmoneyInqueryResponse> PaymentInquery(string trasactionId, string referenceId)
    {
        var client = new HttpClient();
        var log = new ApplicationLog();
        log.BankCode = "nagad";
        log.Service = "nagad_AddmoneyInquery_Request";
        log.TransactionId = trasactionId;
        try
        {

            log.Status = TxnStatus.Processing.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/dfs/verify/payment/{referenceId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json"); ;
            log.Request = await request.ToHttpLog();
            await _unitOfWork.ApplicationLogRepository.Add(log);
            await _unitOfWork.CompleteAsync();
            Log.Information("========== Nagad Inquery Api Request: {Request}", log.Request);

            var response = await client.SendAsync(request);

            log.Response = await response.ToHttpLog();
            Log.Information("========== Nagad Inquery Api Response: {Response}", log.Response);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseObj = await response.Content.ReadAsAsync<NagadTransactionInqueryResponse>();
                log.Status = TxnStatus.Success.ToString();
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();

                if (responseObj.status == "Success")
                {
                    return new BankAddmoneyInqueryResponse
                    {
                        StatusCode = 200,
                        TransactionStatus = TxnStatus.Success.ToString(),
                        BankTransactionId = responseObj.paymentRefId,
                        Message = "Transaction successfull."
                    };
                }
                else if (responseObj.status == "Aborted" || responseObj.status == "Ready")
                {
                    return new BankAddmoneyInqueryResponse
                    {
                        StatusCode = 200,
                        TransactionStatus = TxnStatus.Fail.ToString(),
                        BankTransactionId = "",
                        Message = "Transaction fail."
                    };
                }
                else
                {
                    return null;
                }
             
            }
            else
            {
                log.Status = TxnStatus.Fail.ToString();
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();
             
                return new BankAddmoneyInqueryResponse { StatusCode = 300,  Message = "" };
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Mtb_AddmoneyInquery_Request_Exception");
            if (log.Id > 0)
            {
                log.Response = ex.Message;
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                log.Response = ex.Message;
                await _unitOfWork.ApplicationLogRepository.Add(log);
                await _unitOfWork.CompleteAsync();
            }

            return new BankAddmoneyInqueryResponse { StatusCode = 300, Message = ex.Message };
        }
    }


}
