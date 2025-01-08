using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class TranCheckoutInformation
{
    public long Id { get; set; }

    public string Status { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public Guid Identifier { get; set; }

    public string TransactionReferenceId { get; set; } = null!;

    public string TransactionOrderId { get; set; } = null!;

    public decimal? Amount { get; set; }

    public long? SenderId { get; set; }

    public long? ReceiverId { get; set; }

    public string ReturnUrl { get; set; } = null!;

    public string Otp { get; set; } = null!;

    public DateTime? OtpCreatedDate { get; set; }

    public int? OtpResendCount { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string LastUpdatedBy { get; set; } = null!;
}
