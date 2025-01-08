namespace PSO.CheckoutDemo.Web.Models
{
    public class PaymentRediractResponseModel
    {
        public string status { get; set; }
        public string apiResponseCode { get; set; }
        public int httpStatusCode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string redirectUrl { get; set; }
    }
}
