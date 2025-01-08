namespace DAL.Models.Response;

public class TransactionConfirmResponse
{
    public string TransactionId { get; set; }
    public string SuccessReturnUrl { get; set; }
    public string FailReturnUrl { get; set; }
    public string CancelReturnUrl { get; set; }
    public string TransactionType { get; set; }
    public string TransactionStatus { get; set; }
    public string TransactionOrderId { get; set; }
    public string ReferenceTransactionId { get; set; }
    public string Identifier { get; set; }
    public string TransactionCurrencyCode { get; set; }
    public DateTime TransactionCompletionDateTime { get; set; }
    public decimal TransactionAmount { get; set; }
}
