using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class AspNetUser
{
    public long Id { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string FullName { get; set; } = null!;

    public string? Userid { get; set; }

    public string? User_name { get; set; }

    public string? User_designation { get; set; }

    public string? Department { get; set; }

    public string? User_email { get; set; }

    public string? Status { get; set; }

    public string? Resign_date { get; set; }

    public string? First_Name { get; set; }

    public string? Last_Name { get; set; }

    public string? Father_Name { get; set; }

    public string? Mother_Name { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Post_Code { get; set; }

    public string? Country { get; set; }

    public string? Permanent_Address { get; set; }

    public string? Permanent_City { get; set; }

    public string? Permanent_state { get; set; }

    public string? Permanent_Post { get; set; }

    public string? Permanent_Country { get; set; }

    public string? Religion { get; set; }

    public string? DOB { get; set; }

    public string? MaritalStatus { get; set; }

    public string? Children { get; set; }

    public string? Gender { get; set; }

    public string? Blood_Group { get; set; }

    public string? Homephone { get; set; }

    public string? Workphone { get; set; }

    public string? HandSet { get; set; }

    public string? Join_Date { get; set; }

    public string? Confrim_Date { get; set; }

    public string? TIN { get; set; }

    public string? Section { get; set; }

    public string? Office { get; set; }

    public string? WorkLocation { get; set; }

    public bool IsLoginWithAD { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
