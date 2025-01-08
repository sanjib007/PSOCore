using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MenuAndPermissionSetup
{
    public long Id { get; set; }

    public int? ParentId { get; set; }

    public string? HeadTitle1 { get; set; }

    public int? Level { get; set; }

    public string? Title { get; set; }

    public string? Path { get; set; }

    public string? Icon { get; set; }

    public string? Type { get; set; }

    public bool? HorizontalList { get; set; }

    public bool? Active { get; set; }

    public string? bookmark { get; set; }

    public string? FeatureName { get; set; }

    public string? ControllerName { get; set; }

    public string? MethodName { get; set; }

    public string? MethodType { get; set; }

    public string? ApplicationName { get; set; }

    public string? ApplicationBaseUrl { get; set; }

    public int? MenuSequence { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsVisible { get; set; }

    public bool? ShowInMenuItem { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? AllowAnonymous { get; set; }
}
