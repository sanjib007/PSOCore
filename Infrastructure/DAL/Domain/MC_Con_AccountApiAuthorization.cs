using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MC_Con_AccountApiAuthorization
{
    public long Id { get; set; }

    public long? ConAccountApiAuthorizationId { get; set; }

    public long? AccountId { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
