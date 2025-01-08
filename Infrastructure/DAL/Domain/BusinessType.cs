using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class BusinessType
{
    public long Id { get; set; }

    public string? BusinessTypeName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string? LastModifiedBy { get; set; }
}
