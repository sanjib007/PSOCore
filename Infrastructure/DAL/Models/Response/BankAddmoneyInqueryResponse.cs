namespace DAL.Models.Response;

public class BankAddmoneyInqueryResponse
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; }
    public string BankTransactionId { get; set; }
    public string TransactionStatus { get; set; }
}
