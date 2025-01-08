using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MerchantKYCRegistration
{
    public long Id { get; set; }

    public string? NameOfOrganization { get; set; }

    public string? IntroducerWalletId { get; set; }

    public string? NameOfAuthorizedPerson { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? BusinessType { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PresentAddress { get; set; }

    public string? CompanyOrShopAddress { get; set; }

    public string? MerchantCity { get; set; }

    public string? PermanentAddress { get; set; }

    public string? NameOfFather { get; set; }

    public string? NameOfMother { get; set; }

    public string? NameOfSpouse { get; set; }

    public string? Gender { get; set; }

    public string? Occupation { get; set; }

    public string? BankACName { get; set; }

    public string? NameOfTheBank { get; set; }

    public string? ACNumber { get; set; }

    public string? Branch { get; set; }

    public string? District { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string? LastModifiedBy { get; set; }

    public string? Logo { get; set; }

    public string? RoutingNumber { get; set; }

    public string? Status { get; set; }

    public string? Industry { get; set; }

    public string? NatureOfBusiness { get; set; }

    public long? FeeProfileId { get; set; }

    public string? FeeProfileName { get; set; }

    public string? MCC { get; set; }

    public string? RiskRating { get; set; }
}
