using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Gen_BankInformartion
{
    public long Id { get; set; }

    public string? BankName { get; set; }

    public string? BankAccountName { get; set; }

    public string? BankAccountNo { get; set; }

    public string? RoutingNo { get; set; }

    public string? Branch { get; set; }

    public long? AccountId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public bool? IsActive { get; set; }
}
