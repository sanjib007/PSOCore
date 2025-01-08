using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_ConChannelType
{
    public long ChannelTypeId { get; set; }

    public string ChannelTypeName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
