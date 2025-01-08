using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Con_MerchantPortalAuthorization
{
    public long Id { get; set; }

    public long? AccountId { get; set; }

    public string? FullName { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? ClientSecretEnc { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? AccessTokenDate { get; set; }

    public DateTime? RefreshTokenDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
