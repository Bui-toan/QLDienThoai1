using System;
using System.Collections.Generic;

namespace QLDienThoai.Models;

public partial class DanhGia
{
    public int IdDanhGia { get; set; }

    public int? IdSanPham { get; set; }

    public string? Comment { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? Star { get; set; }

    public virtual SanPham? IdSanPhamNavigation { get; set; }
}
