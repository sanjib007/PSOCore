namespace PSO.Merchant.Spa.Models.Response;


public class TransactionReportReponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public TransactionReportReponseData data { get; set; }
}

public class TransactionReportReponseData
{
    public int total { get; set; }
    public List<TransactionReportReponseDataList> list { get; set; }
}

public class TransactionReportReponseDataList
{
    public decimal transactionFee { get; set; }
    public decimal transactionAmount { get; set; }
    public string transactionDate { get; set; }
    public string transactionStatus { get; set; }
    public string transactionId { get; set; }
    public string transactionOrderId { get; set; }
    public string settlementStatus { get; set; }
    public DateTime? settlementDate { get; set; }
    public bool isRequestedForSettlement { get; set; }
    public string transactionCurrencyCode { get; set; }
    public string channel { get; set; }
    public string transactionType { get; set; }
}

