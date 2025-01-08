using DAL.Models.Request.Merchant;
using PSO.Merchant.Api.Models.Response;
using System.Net.Http;
using Utility.Extentions;

namespace PSO.Merchant.Api.Service;

public interface ICoreTransactionService
{
    Task<BaseResponse> DoRefund(TransactionRequest request);
    Task<BaseResponse> DoVoid(TransactionRequest request);
    Task<TransactionReportReponse> GetTransactionReport(long accountId,
          string? transactionId,
        DateTime? startDate,
        DateTime? endDate,
        string? transactionStatus,
        int pageNumber = 1,
        int pageSize = 10);
    Task<BaseResponse> SettlementRequest(TransactionRequest request);
}

public class CoreTransactionService(IConfiguration _configuration, HttpClient _httpClient) : ICoreTransactionService
{
    public async Task<TransactionReportReponse> GetTransactionReport(long accountId,
           string? transactionId,
        DateTime? startDate,
        DateTime? endDate,
        string? transactionStatus,
        int pageNumber = 1,
        int pageSize = 10)
    {

        var response = await _httpClient.GetDataAsJsonAsync<TransactionReportReponse>(_configuration.GetSection("AppConfig:CoreBaseUrl").Value + $"/api/v1/MerchantReport/Transactions?accountId={accountId}" +
            $"&transactionId={transactionId}" +
            $"&startDate={startDate}" +
            $"&endDate={endDate}" +
            $"&transactionStatus={transactionStatus}" +
            $"&pageNumber={pageNumber}" +
            $"&pageSize={pageSize}");
        return response;
    }
    public async Task<BaseResponse> DoVoid(TransactionRequest request)
    {
        var response = await _httpClient.PostDataAsJsonAsync<BaseResponse>(_configuration.GetSection("AppConfig:CoreBaseUrl").Value + $"/api/v1/MerchantReport/DoVoid", request);
        return response;
    }
    public async Task<BaseResponse> DoRefund(TransactionRequest request)
    {
        var response = await _httpClient.PostDataAsJsonAsync<BaseResponse>(_configuration.GetSection("AppConfig:CoreBaseUrl").Value + $"/api/v1/MerchantReport/DoRefund", request);
        return response;
    }
    public async Task<BaseResponse> SettlementRequest(TransactionRequest request)
    {
        var response = await _httpClient.PostDataAsJsonAsync<BaseResponse>(_configuration.GetSection("AppConfig:CoreBaseUrl").Value + $"/api/v1/MerchantReport/SettlementRequest", request);
        return response;
    }
}
