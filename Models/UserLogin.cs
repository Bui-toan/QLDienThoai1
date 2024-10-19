﻿using System;
using System.Collections.Generic;

namespace QLDienThoai.Models;

public partial class UserLogin
{
    public string LoginProvider { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
