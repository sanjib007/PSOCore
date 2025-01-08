﻿using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Con_FeeProfileDetail
{
    public long FeeProfileDetailsId { get; set; }

    public long? FeeProfileId { get; set; }

    public long? ChannelId { get; set; }

    public string? MCC { get; set; }

    public bool? IsPercentage { get; set; }

    public decimal? FeeAmount { get; set; }

    public bool? IsSlabRequired { get; set; }

    public decimal? FeeAmountFixed { get; set; }

    public decimal? FeeAmountInPercentage { get; set; }

    public decimal? StaticMaximumFeeAmount { get; set; }

    public bool? IsMaximumApplicable { get; set; }

    public decimal? VATDiductPercentOnFeeAmount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
