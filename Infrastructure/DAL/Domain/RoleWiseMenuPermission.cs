using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class RoleWiseMenuPermission
{
    public long Id { get; set; }

    public long MenuSetupId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? UserId { get; set; }
}
