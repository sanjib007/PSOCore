using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_Con_ChannelType
{
    public long Id { get; set; }

    public long? ChannelTypeId { get; set; }

    public string? ChannelTypeName { get; set; }

    public string? AccountType { get; set; }

    public string? Logo { get; set; }

    public int? Serial { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
