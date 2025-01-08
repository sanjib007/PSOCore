using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_ConCostProfileDetailsSlabPolicy
{
    public long CostProfileDetailSlabPolicyId { get; set; }

    public long CostProfileDetailId { get; set; }

    public string Description { get; set; } = null!;

    public decimal? MinAmount { get; set; }

    public decimal? MaxAmount { get; set; }

    public decimal? SegmentAmount { get; set; }

    public decimal? CostAmountFixed { get; set; }

    public decimal? CostAmountInPercentage { get; set; }

    public decimal? StaticMaximumCostAmount { get; set; }

    public bool? IsMaximumApplicable { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
