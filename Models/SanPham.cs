using System;
using System.Collections.Generic;

namespace QLDienThoai.Models;

public partial class SanPham
{
    public int IdBanPham { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? BrandId { get; set; }

    public string? Images { get; set; }

    public int? CategoriesId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Categories { get; set; }

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
