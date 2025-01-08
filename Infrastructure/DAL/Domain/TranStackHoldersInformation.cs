using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class TranStackHoldersInformation
{
    public long StackHoldersInformationId { get; set; }

    public string StakeholderName { get; set; } = null!;

    public string StackHolderType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string BankCode { get; set; } = null!;

    public long? AccountNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string LastUpdatedBy { get; set; } = null!;
}
