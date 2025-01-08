namespace PSO.Checkout.Spa.Models.Response;


public class GetIdentifierApiResponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public GetIdentifierResponseData? data { get; set; }
}

public class GetIdentifierResponseData
{
    public GetIdentifierApiResponseCheckoutdetails checkoutDetails { get; set; }
    public List<GetIdentifierApiResponsePaymentacceptlist> paymentAcceptList { get; set; }
}

public class GetIdentifierApiResponseCheckoutdetails
{
    public string merchantName { get; set; }
    public string status { get; set; }
    public string identifier { get; set; }
    public string referenceId { get; set; }
    public string orderId { get; set; }
    public decimal amount { get; set; }
    public string billerPhone { get; set; }
    public string logo { get; set; }
    public string billerName { get; set; }
    public string billingAddress { get; set; }
}

public class GetIdentifierApiResponsePaymentacceptlist
{
    public int channelTypeId { get; set; }
    public string channelTypeName { get; set; }
    public string accountType { get; set; }
    public string bankCode { get; set; }
    public object logo { get; set; }
}
