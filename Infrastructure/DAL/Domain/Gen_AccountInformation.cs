using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Gen_AccountInformation
{
    public long AccountId { get; set; }

    public string? FullName { get; set; }

    public string? Title { get; set; }

    public long? FeeProfileId { get; set; }

    public long? LimitProfileId { get; set; }

    public long? PaymentAcceptProfileId { get; set; }

    public string? FathersName { get; set; }

    public string? MothersName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? NationalId { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? PoliceStation { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? KycImage { get; set; }

    public string? NationalIdFront { get; set; }

    public string? NationalIdBack { get; set; }

    public bool? IsNationalIdVerified { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyShortName { get; set; }

    public string? CompanyType { get; set; }

    public string? ProductsOrServicesDetails { get; set; }

    public string? CompanyWebsite { get; set; }

    public string? CompanyPhone { get; set; }

    public string? CompanyFax { get; set; }

    public string? CompanyEmail { get; set; }

    public string? CompanyLogo { get; set; }

    public string? CompanyAddress1 { get; set; }

    public string? CompanyAddress2 { get; set; }

    public string? CompanyThana { get; set; }

    public string? CompanyDistrict { get; set; }

    public string? CompanyPostalCode { get; set; }

    public string? NidNumber { get; set; }

    public string? NidPIN { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ProfileImage { get; set; }

    public string? SecondaryMobile { get; set; }

    public string? SecondaryEmail { get; set; }

    public string? Occupation { get; set; }

    public string? Organization { get; set; }

    public string? Designation { get; set; }

    public DateTime? LastPasswordChangedDate { get; set; }

    public string? Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public string? ClientSecrect { get; set; }
}
