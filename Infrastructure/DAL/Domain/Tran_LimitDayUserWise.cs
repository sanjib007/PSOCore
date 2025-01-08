using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_LimitDayUserWise
{
    public long LimitDayUserWiseId { get; set; }

    public long AccountId { get; set; }

    public DateOnly? Day { get; set; }

    public int? Month { get; set; }

    public int? year { get; set; }

    public long? TransactionTypeId { get; set; }

    public string? TransactionType { get; set; }

    public int? NumberOfTransactionPerDay { get; set; }

    public decimal? TransactionAmountPerDay { get; set; }

    public string? LastTransactionId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
