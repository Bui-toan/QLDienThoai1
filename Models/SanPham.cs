using QLDienThoai.Respository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDienThoai.Models;

public partial class SanPham
{
	public int IdSanPham { get; set; }
	[Required(ErrorMessage = "Yêu cầu nhập tên sản phẩm")]
	public string? Name { get; set; }
	[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập mô tả sản phẩm")]
	public string? Description { get; set; }
	[Required(ErrorMessage = "Yêu cầu nhập giá sản phẩm")]
	[Range(0.01, double.MaxValue)]
	[Column(TypeName = "Decimal(8,2)")]
	public decimal? Price { get; set; }
	[Required, Range(1, int.MaxValue, ErrorMessage = "Chọn 1 thương hiệu")]
	public int? BrandId { get; set; }
	public string? Slug { get; set; }
	public string? Images { get; set; }

    [NotMapped]
    [FileExtension]
    public IFormFile? ImageUpload { get; set; }
    [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn 1 danh mục")]
    public int? CategoriesId { get; set; }

	public virtual Brands? Brand { get; set; }

	public virtual Categories? Categories { get; set; }

	public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}
