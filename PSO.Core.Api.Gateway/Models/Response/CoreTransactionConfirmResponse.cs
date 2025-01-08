namespace PSO.Core.Api.Gateway.Models.Response;

public class CoreTransactionConfirmResponse
{
    public int statusCode { get; set; } 
    public string message { get; set; }
    public CoreTransactionConfirmResponseData data { get; set; }
}
public class CoreTransactionConfirmResponseData
{
    public string transactionId { get; set; }
    public string successReturnUrl { get; set; }
    public string failReturnUrl { get; set; }
    public string cancelReturnUrl { get; set; }
    public string transactionType { get; set; }
    public string transactionStatus { get; set; }
    public string transactionOrderId { get; set; }
    public string referenceTransactionId { get; set; }
    public string identifier { get; set; }
    public string transactionCurrencyCode { get; set; }
    public DateTime transactionCompletionDateTime { get; set; }
    public decimal transactionAmount { get; set; }
}