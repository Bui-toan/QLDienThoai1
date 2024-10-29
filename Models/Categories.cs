using System.ComponentModel.DataAnnotations;

namespace QLDienThoai.Models;

public partial class Categories
{
	[Key]
	public int CategoriesId { get; set; }
	[Required(ErrorMessage ="Yêu cầu nhập tên danh mục")]
	public string? Name { get; set; }
	[Required(ErrorMessage = "Yêu cầu nhập mô tả danh mục")]
	public string? Description { get; set; }

	public string? Slug { get; set; }

	public string? Status { get; set; }

	public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
