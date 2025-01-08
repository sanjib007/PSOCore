using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_Con_CostProfile
{
    public long Id { get; set; }

    public long? CostProfileId { get; set; }

    public string? ProfileName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
