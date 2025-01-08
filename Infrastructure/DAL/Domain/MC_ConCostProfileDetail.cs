using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_ConCostProfileDetail
{
    public long CostProfileDetailId { get; set; }

    public long? CostProfileId { get; set; }

    public long? ChannelId { get; set; }

    public string MCC { get; set; } = null!;

    public bool? IsPercentage { get; set; }

    public decimal? CostAmount { get; set; }

    public bool? IsSlabRequired { get; set; }

    public decimal? CostAmountFixed { get; set; }

    public decimal? CostAmountInPercentage { get; set; }

    public decimal? StaticMaximumCostAmount { get; set; }

    public bool? IsMaximumApplicable { get; set; }

    public decimal? VATDeductPercentOnCostAmount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
