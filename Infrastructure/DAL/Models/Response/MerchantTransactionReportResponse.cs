using static Utility.Enums.SystemEnum;

namespace DAL.Models.Response;

public class MerchantTransactionReportResponse
{
    public decimal TransactionFee { get; set; }
    public decimal TransactionAmount { get; set; }
    public string TransactionDate { get; set; }
    public string TransactionStatus { get; set; }
    public string SettlementStatus { get; set; }
    public string TransactionId { get; set; }
    public string TransactionOrderId { get; set; }
    public DateTime? SettlementDate { get; set; }
    public bool IsRequestedForSettlement { get; set; }
    public string TransactionCurrencyCode { get; set; }
    public string Channel { get; set; }
    public string TransactionType { get; set; }

    // Additional properties or methods can be added as needed
}