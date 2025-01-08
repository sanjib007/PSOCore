using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_DailyBalance
{
    public long DaillyBalanceId { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public int Day { get; set; }

    public long AccountId { get; set; }

    public string? AccountName { get; set; }

    public decimal OpeningBalance { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public decimal ClosingBalance { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
