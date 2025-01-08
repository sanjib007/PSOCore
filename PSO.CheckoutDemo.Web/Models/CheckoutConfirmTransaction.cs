namespace PSO.CheckoutDemo.Web.Models
{
    public class CheckoutConfirmTransactionRequest
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionOrderId { get; set; } 
        public string ReferenceTransactionId { get; set; }
        public string TransactionCompletionDateTime { get; set; }
        public string Identifier { get; set; }
        public string TransactionCurrencyCode { get; set; }
        public string TransactionType { get; set; }
    }
}
