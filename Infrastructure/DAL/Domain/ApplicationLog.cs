using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class ApplicationLog
{
    public long Id { get; set; }

    public string? Service { get; set; }

    public string? BankCode { get; set; }

    public string? TransactionId { get; set; }

    public string? ReferenceId { get; set; }

    public string? Request { get; set; }

    public string? Response { get; set; }

    public string? Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
