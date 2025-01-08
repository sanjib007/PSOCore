namespace FinPay.Web.Models
{
    public class GetCheckoutInfoResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public GetCheckoutInfoDataResponse data { get; set; }
    }
    public class GetCheckoutInfoDataResponse
    {
        public string merchantName { get; set; }
        public string orderId { get; set; }
        public decimal amount { get; set; }
        public string redirectURL { get; set; }
    }
}
