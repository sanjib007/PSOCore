using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_LimitPackageDetail
{
    public long LimitPackageDetailId { get; set; }

    public long? LimitPackageId { get; set; }

    public long? ChannelId { get; set; }

    public decimal? MinTransactionAmount { get; set; }

    public decimal? MaxTransactionAmount { get; set; }

    public decimal? MaxTransactionAmountPerDay { get; set; }

    public decimal? MaxTransactionAmountPerMonth { get; set; }

    public int? MaxNumberOfTransactionPerDay { get; set; }

    public int? MaxNumberOfTransactionPerMonth { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
