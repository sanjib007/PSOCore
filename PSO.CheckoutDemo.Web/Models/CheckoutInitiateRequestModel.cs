namespace PSO.CheckoutDemo.Web.Models
{
    public class CheckoutInitiateRequestModel
    {
        public decimal Amount { get; set; }
    }
    public class CheckoutApiResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public CheckoutApiResponseData data { get; set; }
    }

    public class CheckoutApiResponseData
    {
        public string gatewayUrl { get; set; }
    }
}
