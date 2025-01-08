using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_StackHoldersInformation
{
    public long StackHoldersInformationId { get; set; }

    public string? StakeholderName { get; set; }

    public string? StackHolderType { get; set; }

    public string? Description { get; set; }

    public string? BankCode { get; set; }

    public long? AccountNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
