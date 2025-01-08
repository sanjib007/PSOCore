using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_ConFeeProfile
{
    public long FeeProfileId { get; set; }

    public string ProfileName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
