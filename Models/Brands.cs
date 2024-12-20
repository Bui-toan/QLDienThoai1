﻿namespace QLDienThoai.Models;

public partial class Brands
{
	public int BrandId { get; set; }

	public string? Name { get; set; }

	public string? Description { get; set; }

	public string? Slug { get; set; }

	public string? Status { get; set; }

	public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
