using System;
using System.Collections.Generic;

namespace DAL.Domain;

public partial class RSAEncryptDataDuplicationCheck
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
