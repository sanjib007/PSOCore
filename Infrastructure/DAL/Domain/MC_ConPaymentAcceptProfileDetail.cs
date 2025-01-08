using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_ConPaymentAcceptProfileDetail
{
    public long PaymentAcceptProfileDetailsId { get; set; }

    public long? ChannelId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
