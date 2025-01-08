using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Con_PaymentAcceptProfileDetail
{
    public long PaymentAcceptProfileDetailsId { get; set; }

    public long? PaymentAcceptProfileId { get; set; }

    public long? ChannelTypeId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
