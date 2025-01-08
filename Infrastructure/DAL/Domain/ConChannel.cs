using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class ConChannel
{
    public long ChannelId { get; set; }

    public string ChannelName { get; set; } = null!;

    public DateTime? ActiveStartDate { get; set; }

    public DateTime? ActiveEndDate { get; set; }

    public long MC_Id { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
