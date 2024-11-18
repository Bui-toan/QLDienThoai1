using System;
using System.Collections.Generic;

namespace QLDienThoai.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
