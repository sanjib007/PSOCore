using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class BusinessTypeWiseDocumentMapping
{
    public long Id { get; set; }

    public long? BusinessTypeId { get; set; }

    public long? DocumentId { get; set; }

    public string? ImageFieldTitle { get; set; }

    public bool? ImageFieldIsRequired { get; set; }

    public bool? ImageFieldIsVisible { get; set; }

    public string? NumberFieldTitle { get; set; }

    public bool? NumberFieldIsRequired { get; set; }

    public bool? NumberFieldIsVisible { get; set; }

    public string? ExpireDateFieldTitle { get; set; }

    public bool? ExpireDateFieldIsRequired { get; set; }

    public bool? ExpireDateFieldIsVisible { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string? LastModifiedBy { get; set; }
}
