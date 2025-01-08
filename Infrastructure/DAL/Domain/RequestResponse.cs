using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class RequestResponse
{
    public long Id { get; set; }

    public string? Request { get; set; }

    public string? Response { get; set; }

    public string? UserId { get; set; }

    public string? ErrorLog { get; set; }

    public string? MethodName { get; set; }

    public string? RequestedIP { get; set; }

    public string? FullRoute { get; set; }

    public string? ActionVerb { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string? OldData { get; set; }

    public string? NewData { get; set; }
}
