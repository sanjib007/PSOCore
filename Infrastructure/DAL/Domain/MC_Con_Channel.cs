using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_Con_Channel
{
    public long Id { get; set; }

    public long? ChannelId { get; set; }

    public string? ChannelName { get; set; }

    public long? ChannelTypeId { get; set; }

    public DateTime? ActiveStartDate { get; set; }

    public DateTime? ActiveEndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
