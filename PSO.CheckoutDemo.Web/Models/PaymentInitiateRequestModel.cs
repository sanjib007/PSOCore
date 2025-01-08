namespace PSO.CheckoutDemo.Web.Models
{
    public class PaymentInitiateRequestModel
    {
        public string orgCode { get; set; }
        public string returnUrl { get; set; }
        public string description { get; set; }
        public string customerId { get; set; }
        public string customerWalletNo { get; set; }
        public string merchantImg { get; set; }
        public string transactionTrackingNo { get; set; }
        public string billingAddress { get; set; }
        public string invoiceNo { get; set; }
        public decimal amount { get; set; }
    }
}
