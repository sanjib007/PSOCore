namespace PSO.Checkout.Web.Models.Response;

public record GetIdentifierResponse(int statusCode, string message, GetIdentifierResponseData data);
public class GetIdentifierResponseData
{
    public string status { get; set; }
    public string identifier { get; set; }
    public string referenceId { get; set; }
    public string orderId { get; set; }
    public decimal amount { get; set; }
    public string billerPhone { get; set; }
    public string billerName { get; set; }
    public string billingAddress { get; set; }
    public string merchantName { get; set; }
}
