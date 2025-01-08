namespace DAL.Models.Response;

public class MasterInqueryApiReponse
{
    public string _3dsAcsEci { get; set; }
    public float amount { get; set; }
    public MasterInqueryApiReponseAuthentication authentication { get; set; }
    public string authenticationStatus { get; set; }
    public string authenticationVersion { get; set; }
    public MasterInqueryApiReponseChargeback chargeback { get; set; }
    public DateTime creationTime { get; set; }
    public string currency { get; set; }
    public string description { get; set; }
    public MasterInqueryApiReponseDevice device { get; set; }
    public string id { get; set; }
    public DateTime lastUpdatedTime { get; set; }
    public string merchant { get; set; }
    public float merchantAmount { get; set; }
    public string merchantCategoryCode { get; set; }
    public string merchantCurrency { get; set; }
    public string result { get; set; }
    public MasterInqueryApiReponseSourceoffunds sourceOfFunds { get; set; }
    public string status { get; set; }
    public float totalAuthorizedAmount { get; set; }
    public float totalCapturedAmount { get; set; }
    public float totalDisbursedAmount { get; set; }
    public float totalRefundedAmount { get; set; }
    public MasterInqueryApiReponseTransaction[] transaction { get; set; }
}

public class MasterInqueryApiReponseAuthentication
{
    public MasterInqueryApiReponse3Ds _3ds { get; set; }
}

public class MasterInqueryApiReponse3Ds
{
    public string acsEci { get; set; }
    public string authenticationToken { get; set; }
    public string transactionId { get; set; }
}

public class MasterInqueryApiReponseChargeback
{
    public int amount { get; set; }
    public string currency { get; set; }
}

public class MasterInqueryApiReponseDevice
{
    public string browser { get; set; }
    public string ipAddress { get; set; }
}

public class MasterInqueryApiReponseSourceoffunds
{
    public MasterInqueryApiReponseProvided provided { get; set; }
    public string type { get; set; }
}

public class MasterInqueryApiReponseProvided
{
    public MasterInqueryApiReponseCard card { get; set; }
}

public class MasterInqueryApiReponseCard
{
    public string brand { get; set; }
    public MasterInqueryApiReponseExpiry expiry { get; set; }
    public string fundingMethod { get; set; }
    public string nameOnCard { get; set; }
    public string number { get; set; }
    public string scheme { get; set; }
    public string storedOnFile { get; set; }
}

public class MasterInqueryApiReponseExpiry
{
    public string month { get; set; }
    public string year { get; set; }
}

public class MasterInqueryApiReponseTransaction
{
    public MasterInqueryApiReponseAuthentication1 authentication { get; set; }
    public MasterInqueryApiReponseDevice1 device { get; set; }
    public string merchant { get; set; }
    public MasterInqueryApiReponseOrder order { get; set; }
    public MasterInqueryApiReponseResponse response { get; set; }
    public string result { get; set; }
    public MasterInqueryApiReponseSourceoffunds1 sourceOfFunds { get; set; }
    public DateTime timeOfLastUpdate { get; set; }
    public DateTime timeOfRecord { get; set; }
    public MasterInqueryApiReponseTransaction1 transaction { get; set; }
    public string version { get; set; }
    public MasterInqueryApiReponseAuthorizationresponse authorizationResponse { get; set; }
    public string gatewayEntryPoint { get; set; }
}

public class MasterInqueryApiReponseAuthentication1
{
    public MasterInqueryApiReponse_3Ds1 _3ds { get; set; }
    public MasterInqueryApiReponse_3Ds2 _3ds2 { get; set; }
    public string acceptVersions { get; set; }
    public float amount { get; set; }
    public string channel { get; set; }
    public string method { get; set; }
    public string payerInteraction { get; set; }
    public string purpose { get; set; }
    public MasterInqueryApiReponseRedirect redirect { get; set; }
    public DateTime time { get; set; }
    public string version { get; set; }
    public string transactionId { get; set; }
}

public class MasterInqueryApiReponse_3Ds1
{
    public string acsEci { get; set; }
    public string authenticationToken { get; set; }
    public string transactionId { get; set; }
}

public class MasterInqueryApiReponse_3Ds2
{
    public string _3dsServerTransactionId { get; set; }
    public string acsTransactionId { get; set; }
    public string directoryServerId { get; set; }
    public string dsTransactionId { get; set; }
    public bool methodCompleted { get; set; }
    public string methodSupported { get; set; }
    public string protocolVersion { get; set; }
    public string requestorId { get; set; }
    public string requestorName { get; set; }
    public string transactionStatus { get; set; }
}

public class MasterInqueryApiReponseRedirect
{
    public string domainName { get; set; }
}

public class MasterInqueryApiReponseDevice1
{
    public string browser { get; set; }
    public string ipAddress { get; set; }
}

public class MasterInqueryApiReponseOrder
{
    public float amount { get; set; }
    public string authenticationStatus { get; set; }
    public MasterInqueryApiReponseChargeback1 chargeback { get; set; }
    public DateTime creationTime { get; set; }
    public string currency { get; set; }
    public string description { get; set; }
    public string id { get; set; }
    public DateTime lastUpdatedTime { get; set; }
    public float merchantAmount { get; set; }
    public string merchantCategoryCode { get; set; }
    public string merchantCurrency { get; set; }
    public string status { get; set; }
    public float totalAuthorizedAmount { get; set; }
    public float totalCapturedAmount { get; set; }
    public float totalDisbursedAmount { get; set; }
    public float totalRefundedAmount { get; set; }
    public MasterInqueryApiReponseValuetransfer valueTransfer { get; set; }
}

public class MasterInqueryApiReponseChargeback1
{
    public int amount { get; set; }
    public string currency { get; set; }
}

public class MasterInqueryApiReponseValuetransfer
{
    public string accountType { get; set; }
}

public class MasterInqueryApiReponseResponse
{
    public string gatewayCode { get; set; }
    public string gatewayRecommendation { get; set; }
    public string acquirerCode { get; set; }
    public string acquirerMessage { get; set; }
    public MasterInqueryApiReponseCardsecuritycode cardSecurityCode { get; set; }
}

public class MasterInqueryApiReponseCardsecuritycode
{
    public string acquirerCode { get; set; }
    public string gatewayCode { get; set; }
}

public class MasterInqueryApiReponseSourceoffunds1
{
    public MasterInqueryApiReponseProvided1 provided { get; set; }
    public string type { get; set; }
}

public class MasterInqueryApiReponseProvided1
{
    public MasterInqueryApiReponseCard1 card { get; set; }
}

public class MasterInqueryApiReponseCard1
{
    public string brand { get; set; }
    public MasterInqueryApiReponseExpiry1 expiry { get; set; }
    public string fundingMethod { get; set; }
    public string nameOnCard { get; set; }
    public string number { get; set; }
    public string scheme { get; set; }
    public string storedOnFile { get; set; }
}

public class MasterInqueryApiReponseExpiry1
{
    public string month { get; set; }
    public string year { get; set; }
}

public class MasterInqueryApiReponseTransaction1
{
    public MasterInqueryApiReponseAcquirer acquirer { get; set; }
    public float amount { get; set; }
    public string authenticationStatus { get; set; }
    public string currency { get; set; }
    public string id { get; set; }
    public string stan { get; set; }
    public string type { get; set; }
    public string authorizationCode { get; set; }
    public string receipt { get; set; }
    public string source { get; set; }
    public string terminal { get; set; }
}

public class MasterInqueryApiReponseAcquirer
{
    public string merchantId { get; set; }
    public int batch { get; set; }
    public string date { get; set; }
    public string id { get; set; }
    public string settlementDate { get; set; }
    public string timeZone { get; set; }
    public string transactionId { get; set; }
}

public class MasterInqueryApiReponseAuthorizationresponse
{
    public string cardSecurityCodeError { get; set; }
    public string commercialCard { get; set; }
    public string commercialCardIndicator { get; set; }
    public string financialNetworkCode { get; set; }
    public string posData { get; set; }
    public string posEntryMode { get; set; }
    public string processingCode { get; set; }
    public string responseCode { get; set; }
    public string stan { get; set; }
    public string transactionIdentifier { get; set; }
}