using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class MenuSetup
{
    public long Id { get; set; }

    public string? MenuName { get; set; }

    public string? MenuIcon { get; set; }

    public string? MenuPath { get; set; }

    public string? FeatureName { get; set; }

    public string? ControllerName { get; set; }

    public string? MethodName { get; set; }

    public string? MethodType { get; set; }

    public string? ApplicationName { get; set; }

    public string? ApplicationBaseUrl { get; set; }

    public int ParentId { get; set; }

    public int MenuSequence { get; set; }

    public bool IsActive { get; set; }

    public bool IsVisible { get; set; }

    public bool ShowInMenuItem { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool AllowAnonymous { get; set; }
}
