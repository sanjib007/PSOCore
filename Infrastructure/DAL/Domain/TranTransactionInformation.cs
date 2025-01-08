using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class TranTransactionInformation
{
    public long TransactionInformationId { get; set; }

    public string TransactionId { get; set; } = null!;

    public long? SenderId { get; set; }

    public long? ReceiverId { get; set; }

    public DateTime? TransactionInnitiationDateTime { get; set; }

    public DateTime? TransactionCompletionDateTime { get; set; }

    public string TransactionType { get; set; } = null!;

    public string TypeCode { get; set; } = null!;

    public string TransactionCurrencyCode { get; set; } = null!;

    public decimal TransactionAmount { get; set; }

    public decimal? FeeAmount { get; set; }

    public decimal? FeeVatamount { get; set; }

    public string TransactionStatus { get; set; } = null!;

    public string TransactionOrderId { get; set; } = null!;

    public bool? IsRequestedForSettlement { get; set; }

    public DateTime? SettlementDate { get; set; }

    public string ReferenceTransactionId { get; set; } = null!;

    public string TransactinPurpose { get; set; } = null!;

    public decimal? Cost { get; set; }

    public string BankProvidedUniqueTransactionId { get; set; } = null!;

    public string BankStan { get; set; } = null!;

    public string Channel { get; set; } = null!;

    public string FeePayee { get; set; } = null!;

    public bool? IsSettled { get; set; }

    public long? SponserWalletId { get; set; }

    public string QErrorMessage { get; set; } = null!;

    public string Trace { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string LastUpdatedBy { get; set; } = null!;

    public bool? IsActive { get; set; }
}
