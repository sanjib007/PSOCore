namespace PSO.Checkout.Spa.Models.Response
{
    public class InitiateTransactionApiReponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public InitiateTransactionApiReponseData data { get; set; }
    }
    public class InitiateTransactionApiReponseData
    {
        public bool isRedirect { get; set; }
        public string redirectUrl { get; set; }
        public string confirmUrl { get; set; }
        public string mastercardSessionId { get; set; }
        public bool isFormRedirect { get; set; }
        public bool isMastercard { get; set; }
        public Dictionary<string, string> formData { get; set; }
    }
}
