namespace DAL.Domain;

public partial class Con_AccountApiInfo
{
    public long Id { get; set; }

    public long? AccountId { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? RefreshToken { get; set; }

    public string? AccessToken { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public long? RoleId { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? RefreshTokenDate { get; set; }

    public DateTime? AccessTokenDate { get; set; }
}
