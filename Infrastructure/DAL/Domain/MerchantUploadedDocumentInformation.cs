using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MerchantUploadedDocumentInformation
{
    public long Id { get; set; }

    public long? MerchantId { get; set; }

    public long? DocumentId { get; set; }

    public long? BusinessTypeId { get; set; }

    public string? DocumentName { get; set; }

    public string? DocumentFileName { get; set; }

    public DateTime? DocumentExpireDate { get; set; }

    public string? DocumentNumber { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string? LastModifiedBy { get; set; }
}
