using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Con_FeeProfileDetailsSlabPolicy
{
    public long FeeProfileDetailsSlabPolicyId { get; set; }

    public long FeeProfileDetailId { get; set; }

    public string? Description { get; set; }

    public decimal? MinAmount { get; set; }

    public decimal? MaxAmount { get; set; }

    public decimal? SegmentAmount { get; set; }

    public decimal? FeeAmountFixed { get; set; }

    public decimal? FeeAmountInPercentage { get; set; }

    public decimal? StaticMaximumFeeAmount { get; set; }

    public bool? IsMaximumApplicable { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
