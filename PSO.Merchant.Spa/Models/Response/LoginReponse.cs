namespace PSO.Merchant.Spa.Models.Response;

public class LoginReponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public LoginReponseData data { get; set; }
}

public class LoginReponseData
{
    public string clientId { get; set; }
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
    public int expireInMin { get; set; }
    public string tokenType { get; set; }
    public string fullName { get; set; }
}



