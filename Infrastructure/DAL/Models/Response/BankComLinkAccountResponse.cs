namespace DAL.Models.Response
{
    public class BankComLinkAccountResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string LinkSessionId { get; set; }
        public string LinkToken { get; set; }
        public bool IsRedirect { get; set; }
        public bool AlreadyLinked { get; set; }
        public string RedirectUrl { get; set; }
    }
    public class BankComAddmoneyResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string AddMoneySessionId { get; set; }
        public string MastercardSessionId { get; set; }
        public string ReferenceId { get; set; }
        public bool IsRedirect { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsFormRedirect { get; set; }
        public Dictionary<string, string> FormData { get; set; }
        public bool IsOTPrequired { get; set; }
    }
}
