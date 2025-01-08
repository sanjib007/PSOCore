using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class ConFeeProfileDetailsSlabPolicy
{
    public long FeeProfileDetailsSlabPolicyId { get; set; }

    public long FeeProfileDetailsId { get; set; }

    public string Description { get; set; } = null!;

    public decimal? MinAmount { get; set; }

    public decimal? MaxAmount { get; set; }

    public decimal? SegmentAmount { get; set; }

    public decimal? FeeAmountFixed { get; set; }

    public decimal? FeeAmountInPercentage { get; set; }

    public decimal? StaticMaximumFeeAmount { get; set; }

    public bool? IsMaximumApplicable { get; set; }

    public decimal? VATDiductPercentOnFeeAmount { get; set; }

    public long MC_Id { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
