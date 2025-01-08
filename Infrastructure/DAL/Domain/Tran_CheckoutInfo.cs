using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_CheckoutInfo
{
    public long Id { get; set; }

    public string? Status { get; set; }

    public string? TransactionId { get; set; }

    public string? Identifier { get; set; }

    public string? ReferenceId { get; set; }

    public string? OrderId { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public long? AccountId { get; set; }

    public long? ReceiverId { get; set; }

    public string? SuccessReturnUrl { get; set; }

    public string? FailReturnUrl { get; set; }

    public string? CancelReturnUrl { get; set; }

    public string? ClientId { get; set; }

    public string? BillerName { get; set; }

    public string? BillerPhone { get; set; }

    public string? BillingAddress { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public string? ChannelType { get; set; }

    public long? ChannelId { get; set; }

    public string? Trace { get; set; }
}
