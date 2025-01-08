using DAL.Domain;
using DAL.Models.Response;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Text;
using Utility;
using static Utility.Enums.SystemEnum;
using Utility.Extentions;
using Microsoft.Extensions.Configuration;

namespace BLL.Services.Communication.Mastercard;

public interface IMastercardCommunicationService
{
    Task<BankComAddmoneyResponse> InitateCheckout(string transactionId, decimal transactionAmount);
    Task<BankAddmoneyInqueryResponse> InqueryCheckout(string transactionId, string referenceId);
}


public class MastercardCommunicationService(IHttpContextAccessor _httpContextAccessor, IConfiguration _configuration, HttpClient _httpClient, IUnitOfWork _unitOfWork) : IMastercardCommunicationService
{
    #region Mastercard - EBL

    public async Task<BankComAddmoneyResponse> InitateCheckout(string transactionId, decimal transactionAmount)
    {
        // populate data
        var baseUrl = _configuration.GetSection("BankCommunication:Mastercard:BaseUrl").Value;
        var merchantId = _configuration.GetSection("BankCommunication:Mastercard:MerchantId").Value;
        var password = _configuration.GetSection("BankCommunication:Mastercard:Password").Value;


        var bankResponse = new BankComAddmoneyResponse();
        var log = new ApplicationLog();
        log.BankCode = "Master";
        log.Service = "InitiateCheckout_Request";

        try
        {
            log.Status = TxnStatus.Processing.ToString();
            var request = new
            {
                apiOperation = "INITIATE_CHECKOUT",
                order = new
                {
                    id = transactionId,
                    currency = "BDT",
                    amount = transactionAmount.ToTruncateDecimal(),
                    description = "TAKA"
                },
                interaction = new
                {
                    operation = "PURCHASE",
                    merchant = new
                    {
                        name = "ETYMOLOGY",
                        url = "https://www.etymology.com.bd"
                        //logo = "https://www.dmoney.com.bd/img/logo.png"

                    },
                    cancelUrl = $"{_configuration.GetSection("AppConfig:CoreApiGatewayUrl").Value}/api/v1/mastercard-cancel",
                    returnUrl = $"{_configuration.GetSection("AppConfig:CoreApiGatewayUrl").Value}/api/v1/mastercard-return"
                }
            };

            var authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"merchant.{merchantId}:{password}"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authorization}");
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/{merchantId}/session");
            httpRequest.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            log.Request = await httpRequest.ToHttpLog();
            await _unitOfWork.ApplicationLogRepository.Add(log);
            await _unitOfWork.CompleteAsync();
            Log.Information("======== Mastercard Api Communication Request: {Request}", log.Request);

            var response = await _httpClient.SendAsync(httpRequest);

            log.Response = await response.ToHttpLog();
            _unitOfWork.ApplicationLogRepository.Update(log);
            await _unitOfWork.CompleteAsync();
            Log.Information("======== Mastercard Api Communication Response: {Response}", log.Response);

            var responseObj = JsonConvert.DeserializeObject<MasterInitiateCheckoutResponse>(await response.Content.ReadAsStringAsync());
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                log.Status = TxnStatus.Success.ToString();
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();

                bankResponse.MastercardSessionId = responseObj.session.id;
                bankResponse.ReferenceId = responseObj.successIndicator;
                bankResponse.StatusCode = 200;
                bankResponse.IsRedirect = true;
                bankResponse.RedirectUrl = "";
                return bankResponse;
            }
            else
            {
                log.Status = TxnStatus.Fail.ToString();
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();

                bankResponse.StatusCode = 100;
                bankResponse.IsRedirect = false;
                bankResponse.Message = responseObj.result;
                return bankResponse;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Master_Checkout_Request_Exception: ");
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

    public async Task<BankAddmoneyInqueryResponse> InqueryCheckout(string transactionId, string referenceId)
    {
        // populate data
        var baseUrl = _configuration.GetSection("BankCommunication:Mastercard:BaseUrl").Value;
        var merchantId = _configuration.GetSection("BankCommunication:Mastercard:MerchantId").Value;
        var password = _configuration.GetSection("BankCommunication:Mastercard:Password").Value;

        var log = new ApplicationLog();
        log.BankCode = "Master";
        log.Service = "Inquery_MasterCard_Request";

        try
        {
            log.Status = TxnStatus.Processing.ToString();
            var request = new
            {

            };

            var authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"merchant.{merchantId}:{password}"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authorization}");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{merchantId}/order/{transactionId}");
            httpRequest.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            log.Request = await httpRequest.ToHttpLog();
            await _unitOfWork.ApplicationLogRepository.Add(log);
            await _unitOfWork.CompleteAsync();
            Log.Information("======== Mastercard Api Communication Request: {Request}", log.Request);

            var response = await _httpClient.SendAsync(httpRequest);

            log.Response = await response.ToHttpLog();
            _unitOfWork.ApplicationLogRepository.Update(log);
            await _unitOfWork.CompleteAsync();
            Log.Information("======== Mastercard Api Communication Response: {Response}", log.Response);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseObj = JsonConvert.DeserializeObject<MasterInqueryApiReponse>(await response.Content.ReadAsStringAsync());
                log.Status = TxnStatus.Success.ToString();
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();

                return new BankAddmoneyInqueryResponse
                {
                    StatusCode = 200,
                    TransactionStatus = TxnStatus.Success.ToString(),
                    BankTransactionId = referenceId,
                    Message = "Transaction successfull."
                };
            }
            else
            {
                log.Status = TxnStatus.Fail.ToString();
                _unitOfWork.ApplicationLogRepository.Update(log);
                await _unitOfWork.CompleteAsync();

                return new BankAddmoneyInqueryResponse
                {
                    StatusCode = 200,
                    TransactionStatus = TxnStatus.Fail.ToString(),
                    BankTransactionId = "",
                    Message = "Transaction fail."
                };
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Master_Checkout_Request_Exception: ");
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
            return new BankAddmoneyInqueryResponse
            {
                StatusCode = 300,
                TransactionStatus = TxnStatus.Processing.ToString(),
                BankTransactionId = "",
                Message = "Transaction fail."
            };
        }

    }

    #endregion

}
