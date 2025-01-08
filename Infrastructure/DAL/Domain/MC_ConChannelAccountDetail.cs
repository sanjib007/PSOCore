using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_ConChannelAccountDetail
{
    public long ChannelAccountDetailId { get; set; }

    public long? ChannelId { get; set; }

    public long? AccountId { get; set; }

    public string MCC { get; set; } = null!;

    public string MID { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
