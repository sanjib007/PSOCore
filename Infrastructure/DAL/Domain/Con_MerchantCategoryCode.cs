namespace DAL.Domain;

public partial class Con_MerchantCategoryCode
{
    public long MerchantCategoryCodeId { get; set; }

    public long? ChennelId { get; set; }

    public string? MCC { get; set; }

    public string? MID { get; set; }

    public string? Provider { get; set; }

    public long? AccountId { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
