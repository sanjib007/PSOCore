using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class TranDayClosingInformation
{
    public long TrnDayClosingInformationId { get; set; }

    public DateTime ClosedDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdatedAt { get; set; }

    public string LastUpdatedBy { get; set; } = null!;
}
