using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_LimitMonthUserWise
{
    public long Id { get; set; }

    public long? AccountId { get; set; }

    public int? Month { get; set; }

    public int? year { get; set; }

    public long? ChannelId { get; set; }

    public int? NumberOfTransactionPerMonth { get; set; }

    public decimal? TransactionAmountPerMonth { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
