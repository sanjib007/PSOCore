namespace PSO.Checkout.Web.Models.Response
{

    public class InitiateCheckoutTransactionResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public InitiateCheckoutTransactionResponseData data { get; set; }
    }

    public class InitiateCheckoutTransactionResponseData
    {
        public string status { get; set; }
        public string identifier { get; set; }
        public string referenceId { get; set; }
        public string orderId { get; set; }
        public string currency { get; set; }
        public decimal amount { get; set; }
        public string billerPhone { get; set; }
        public string billerName { get; set; }
        public string billingAddress { get; set; }
        public string GatewayRoute { get; set; }
        public string TransactionId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionOrderId { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string TransactionCurrencyCode { get; set; }
        public string merchantName { get; set; }
    }

  


}
