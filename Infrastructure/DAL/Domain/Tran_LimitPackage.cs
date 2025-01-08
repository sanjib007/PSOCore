using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class Tran_LimitPackage
{
    public long LimitPackageId { get; set; }

    public string? PackageName { get; set; }

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public string? LastUpdatedBy { get; set; }
}
