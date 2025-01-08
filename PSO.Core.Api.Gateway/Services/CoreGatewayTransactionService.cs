using DAL.Models.Request;
using PSO.Core.Api.Gateway.Models.Response;
using Utility.Extentions;

namespace PSO.Core.Api.Gateway.Services;


public interface ICoreGatewayTransactionService
{
    Task<CoreTransactionConfirmResponse> MastercardReturn(string resultIndicator, string sessionVersion, string checkoutVersion);
    Task<CoreTransactionConfirmResponse> NagadConfirm(string merchant, string order_id, string payment_ref_id, string status, string status_code, string message);
    Task<CoreTransactionConfirmResponse> VisaReturnApprove(VisaPaymentApproveRequest request);
    Task<CoreTransactionConfirmResponse> VisaReturnCancel(VisaCancelRequest request);
}
public class CoreGatewayTransactionService(IConfiguration _configuration, HttpClient _httpClient) : ICoreGatewayTransactionService
{
    #region NAGAD

    public async Task<CoreTransactionConfirmResponse> NagadConfirm(string merchant,
      string order_id,
      string payment_ref_id,
      string status,
      string status_code,
      string message)
    {
        var response = await _httpClient.GetDataAsJsonAsync<CoreTransactionConfirmResponse>(_configuration.GetSection("GlobalConfiguration:Hosts:CoreApi").Value + $"/api/v1/nagad-confirm-return?merchant={merchant}&order_id={order_id}&payment_ref_id={payment_ref_id}&status={status}&status_code={status_code}&message={message}");
        return response;
    }
    #endregion

    #region Mastercard

    public async Task<CoreTransactionConfirmResponse> MastercardReturn(
     string resultIndicator,
     string sessionVersion,
     string checkoutVersion)
    {
        var response = await _httpClient.GetDataAsJsonAsync<CoreTransactionConfirmResponse>(_configuration.GetSection("GlobalConfiguration:Hosts:CoreApi").Value + $"/api/v1/mastercard-return?resultIndicator={resultIndicator}&sessionVersion={sessionVersion}&checkoutVersion={checkoutVersion}");
        return response;
    }
    public async Task<CoreTransactionConfirmResponse> MastercardReturnCancel(string merchant,
    string resultIndicator,
    string sessionVersion,
    string checkoutVersion)
    {
        var response = await _httpClient.GetDataAsJsonAsync<CoreTransactionConfirmResponse>(_configuration.GetSection("GlobalConfiguration:Hosts:CoreApi").Value + $"/api/v1/mastercard-return-cancel?resultIndicator={resultIndicator}&sessionVersion={sessionVersion}&checkoutVersion={checkoutVersion}");
        return response;
    }
    #endregion

    #region VISA
    public async Task<CoreTransactionConfirmResponse> VisaReturnApprove(VisaPaymentApproveRequest request)
    {
        var response = await _httpClient.PostDataAsJsonAsync<CoreTransactionConfirmResponse>(_configuration.GetSection("GlobalConfiguration:Hosts:CoreApi").Value + $"/api/v1/visa-return/approve",request);
        return response;
    }
    public async Task<CoreTransactionConfirmResponse> VisaReturnCancel(VisaCancelRequest request)
    {
        var response = await _httpClient.PostDataAsJsonAsync<CoreTransactionConfirmResponse>(_configuration.GetSection("GlobalConfiguration:Hosts:CoreApi").Value + $"/api/v1/visa-return/cancel", request);
        return response;
    }
    #endregion
}
