namespace PSO.CheckoutDemo.Web.Models
{
    public class CheckoutConfirmTransactionRequest
    {
        public string status { get; set; }
        public string message { get; set; }
        public string transactionTrackingNo { get; set; }
        public double amount { get; set; }
        public string transactionDate { get; set; }
    }
}
