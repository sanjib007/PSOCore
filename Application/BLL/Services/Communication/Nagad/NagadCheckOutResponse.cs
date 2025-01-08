namespace BLL.Services.Communication.Nagad;

public class Nagad_CaheckOutCompleteApiResponse
{
    public string callBackUrl { get; set; }
    public string status { get; set; }
}


public class Nagad_CheckOutComplete
{
    public string merchantCallbackURL { get; set; }
    //public string additionalMerchantInfo { get; set; }
    public string sensitiveData { get; set; }
    public string signature { get; set; }
}
public class Nagad_CheckOutCompleteSensitive
{
    public string merchantId { get; set; }
    public string orderId { get; set; }
    public string currencyCode { get; set; }
    public string amount { get; set; }
    public string challenge { get; set; }
}
public class Nagad_CheckOut
{
    public string accountNumber { get; set; }
    public string dateTime { get; set; }
    public string sensitiveData { get; set; }
    public string signature { get; set; }
}

public class Nagad_SensitiveDataModel
{
    public string merchantId { get; set; }
    public string datetime { get; set; }
    public string orderId { get; set; }
    //public string acceptDateTime { get; set; }
    //public string random { get; set; }
    //public string paymentReferenceId { get; set; }
    public string challenge { get; set; }

}

public class Nagad_CheckoutApiResponse
{
    public string sensitiveData { get; set; }
    public string signature { get; set; }
}

public class Nagad_CheckoutApiSensitiveResponse
{
    public string paymentReferenceId { get; set; }
    public string challenge { get; set; }
    public string acceptDateTime { get; set; }
}


public class NagadTransactionInqueryResponse
{
    public string merchantId { get; set; }
    public string orderId { get; set; }
    public string paymentRefId { get; set; }
    public string amount { get; set; }
    public string clientMobileNo { get; set; }
    public string merchantMobileNo { get; set; }
    public string orderDateTime { get; set; }
    public object issuerPaymentDateTime { get; set; }
    public object issuerPaymentRefNo { get; set; }
    public object additionalMerchantInfo { get; set; }
    public string status { get; set; }
    public string statusCode { get; set; }
    public object cancelIssuerDateTime { get; set; }
    public object cancelIssuerRefNo { get; set; }
    public string serviceType { get; set; }
}
