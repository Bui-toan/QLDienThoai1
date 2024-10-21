using System;
using System.Collections.Generic;

namespace QLDienThoai.Models;

public partial class SanPham
{
    public int IdSanPham { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? BrandId { get; set; }

    public string? Images { get; set; }

    public int? CategoriesId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Categories? Categories { get; set; }

    public virtual ICollection<DanhGia> DanhGia { get; set; } = new List<DanhGia>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
