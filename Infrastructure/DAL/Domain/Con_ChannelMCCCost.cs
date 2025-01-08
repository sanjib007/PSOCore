namespace DAL.Domain;

public partial class Con_ChannelMCCCost
{
    public long ChannelMCCCostId { get; set; }

    public bool? IsPercentage { get; set; }

    public string? MCC { get; set; }

    public decimal? Cost { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
