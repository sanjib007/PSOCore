using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_SettlementReport
{
    public long Id { get; set; }

    /// <summary>
    /// Download Summary/Download Details/Upload Details
    /// </summary>
    public string? ReportType { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public DateTime? GenerationUploadedDate { get; set; }

    public string? FileName { get; set; }

    public string? Location { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
