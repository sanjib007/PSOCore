using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class ConChannelMCCCost
{
    public long ChannelMCCCostId { get; set; }

    public bool? IsPercentage { get; set; }

    public string MCC { get; set; } = null!;

    public decimal? Cost { get; set; }

    public long MC_Id { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
