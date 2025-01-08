using System.Collections.Specialized;

namespace PSO.Checkout.Web.Models.Response;

public record CompleteCheckoutTransactionResponse(int statusCode, string message, CompleteCheckoutTransactionResponseData data);
public record CompleteCheckoutTransactionResponseData(string redirectUrl, CompleteCheckoutTransactionData transactionData);
public class CompleteCheckoutTransactionData
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string TransactionId { get; set; }
    public decimal TransactionAmount { get; set; }
    public string TransactionStatus { get; set; }
    public string TransactionOrderId { get; set; }
    public string ReferenceTransactionId { get; set; }
    public DateTime TransactionCompletionDateTime { get; set; }
    public string Identifier { get; set; }
    public string TransactionCurrencyCode { get; set; }
    public string TransactionType { get; set; }
}