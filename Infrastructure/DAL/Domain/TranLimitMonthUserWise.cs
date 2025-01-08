using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class TranLimitMonthUserWise
{
    public long Id { get; set; }

    public long? AccountId { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public string TransactionTypeId { get; set; } = null!;

    public string TransactionType { get; set; } = null!;

    public int? NumberOfTransactionPerMonth { get; set; }

    public decimal? TransactionAmountPerMonth { get; set; }

    public string LastTransactionId { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string LastUpdatedBy { get; set; } = null!;
}
