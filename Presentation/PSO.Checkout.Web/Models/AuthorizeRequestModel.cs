namespace TapCore.Web.Models
{
    public class AuthorizeRequestModel
    {
        public string Identifier { get; set; }
        public string RedirectURL { get; set; }
        public string Password { get; set; }
        public string UserNumber { get; set; }
        public string Otp { get; set; }


    }



    public class MasterInitiateCheckoutResponse
    {
        public string checkoutMode { get; set; }
        public string merchant { get; set; }
        public string result { get; set; }
        public MasterInitiateCheckoutResponseSession session { get; set; }
        public string successIndicator { get; set; }
    }

    public class MasterInitiateCheckoutResponseSession
    {
        public string id { get; set; }
        public string updateStatus { get; set; }
        public string version { get; set; }
    }

}
