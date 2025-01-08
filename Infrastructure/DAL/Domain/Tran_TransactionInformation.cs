using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_TransactionInformation
{
    public long TransactionInformationId { get; set; }

    public string? Identifier { get; set; }

    public string? TransactionId { get; set; }

    public string? ReferenceTransactionId { get; set; }

    public string? TransactionOrderId { get; set; }

    public string? TransactionType { get; set; }

    public decimal? TransactionAmount { get; set; }

    public string? TransactionStatus { get; set; }

    public long? AccountId { get; set; }

    public long? IssuerId { get; set; }

    public long? AcquirerId { get; set; }

    public DateTime? TransactionInnitiationDateTime { get; set; }

    public DateTime? TransactionCompletionDateTime { get; set; }

    public string? BankCode { get; set; }

    public long? ChannelId { get; set; }

    public string? TransactionCurrencyCode { get; set; }

    public decimal? FeeAmount { get; set; }

    public decimal? FeeVatAmount { get; set; }

    public bool? IsRequestedForSettlement { get; set; }

    public DateTime? RequestedForSettlementDate { get; set; }

    public DateTime? SettlementDate { get; set; }

    public string? SettlementStatus { get; set; }

    public string? SettlementRemarks { get; set; }

    public string? TransactinPurpose { get; set; }

    public decimal? Cost { get; set; }

    public string? ChannelUniqueTransactionId { get; set; }

    public string? BankStan { get; set; }

    public string? Channel { get; set; }

    public string? FeePayee { get; set; }

    public bool? IsSettled { get; set; }

    public long? SponserWalletId { get; set; }

    public string? QErrorMessage { get; set; }

    public string? Trace { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public string? GatewayRoute { get; set; }

    public string? DownstremReferenceId { get; set; }

    public long? ChannelTypeId { get; set; }

    public string? ChannelTypeName { get; set; }
}
