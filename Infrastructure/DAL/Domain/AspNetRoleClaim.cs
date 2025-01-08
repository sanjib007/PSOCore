﻿using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class AspNetRoleClaim
{
    public int Id { get; set; }

    public long RoleId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual AspNetRole Role { get; set; } = null!;
}
