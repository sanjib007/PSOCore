namespace PSO.Merchant.Spa.Models.Response;

public class BaseResponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public object data { get; set; }
}

