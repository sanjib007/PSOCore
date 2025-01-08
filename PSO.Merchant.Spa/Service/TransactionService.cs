using PSO.Merchant.Spa.Models.Request;
using PSO.Merchant.Spa.Models.Response;

public interface ITransactionService
{
    Task<DashbordReportResponse> DashbordReport();
    Task<BaseResponse> DoRefund(string transactionId);
    Task<BaseResponse> DoVoid(string transactionId);
    Task<TransactionReportReponse> GetAllTransactions(
        string? transactionId,
      string? startDate,
        string? endDate,
        string? transactionStatus,
        int pageNumber = 1,
        int pageSize = 10);
    Task<BaseResponse> SettlementRequest(string transactionId);
}
public class TransactionService(IConfiguration _configuration, HttpClient _httpClient) : ITransactionService
{
    public async Task<TransactionReportReponse> GetAllTransactions(
        string? transactionId, 
        string? startDate,
        string? endDate,
        string? transactionStatus,
        int pageNumber = 1, 
        int pageSize = 10)
    {
        var response = await _httpClient.GetDataAsJsonAsync<TransactionReportReponse>(_configuration.GetSection("AppConfig:MerchantBaseUrl").Value + $"/api/v1/TransactionReport/GetTransactions" +
            $"?transactionId={transactionId}" +
            $"&startDate={startDate}" +
            $"&endDate={endDate}" +
            $"&transactionStatus={transactionStatus}" +
            $"&pageNumber={pageNumber}" +
            $"&pageSize={pageSize}");
        return response;
    }   
    public async Task<BaseResponse> DoVoid(string transactionId)
    {
        var request = new
        {
            transactionId
        };
        var response = await _httpClient.PostDataAsJsonAsync<BaseResponse>(_configuration.GetSection("AppConfig:MerchantBaseUrl").Value + $"/api/v1/TransactionReport/DoVoid",request);
        return response;
    }

    public async Task<BaseResponse> DoRefund(string transactionId)
    {
        var request = new
        {
            transactionId
        };
        var response = await _httpClient.PostDataAsJsonAsync<BaseResponse>(_configuration.GetSection("AppConfig:MerchantBaseUrl").Value + $"/api/v1/TransactionReport/DoRefund", request);
        return response;
    }
    public async Task<BaseResponse> SettlementRequest(string transactionId)
    {
        var request = new
        {
            transactionId
        };
        var response = await _httpClient.PostDataAsJsonAsync<BaseResponse>(_configuration.GetSection("AppConfig:MerchantBaseUrl").Value + $"/api/v1/TransactionReport/SettlementRequest", request);
        return response;
    }  
    public async Task<DashbordReportResponse> DashbordReport()
    {
        var response = await _httpClient.GetDataAsJsonAsync<DashbordReportResponse>(_configuration.GetSection("AppConfig:MerchantBaseUrl").Value + $"/api/v1/TransactionReport/DashbordReport");
        return response;
    }
}
