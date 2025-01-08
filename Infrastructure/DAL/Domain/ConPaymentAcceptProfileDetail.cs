using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class ConPaymentAcceptProfileDetail
{
    public long PaymentAcceptProfileDetailsId { get; set; }

    public long? ChannelId { get; set; }

    public long MC_Id { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
