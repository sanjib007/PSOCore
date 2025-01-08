using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_DayClosingInformation
{
    public long TrnDayClosingInformationId { get; set; }

    public DateTime ClosedDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
