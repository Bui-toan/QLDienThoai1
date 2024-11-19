using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
using X.PagedList;

namespace QLDienThoai.Controllers
{
	public class ProductController : Controller
	{
		public readonly QldienThoaiContext _dataContext;
		public ProductController(QldienThoaiContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Search(string searchTerm, int? startprice, int? endprice, string sort_by, int page = 1)
		{
			// Lấy danh sách sản phẩm dựa trên từ khóa tìm kiếm
			var products = _dataContext.SanPhams
				.Where(p => string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));

			// Lọc theo khoảng giá (nếu có)
			if (startprice.HasValue && endprice.HasValue)
			{
				products = products.Where(p => p.Price >= startprice && p.Price <= endprice);
			}

			// Sắp xếp theo tiêu chí được chọn
			products = sort_by switch
			{
				"price_increase" => products.OrderBy(p => p.Price),         // Giá tăng dần
				"price_decrease" => products.OrderByDescending(p => p.Price), // Giá giảm dần
				"price_newest" => products.OrderByDescending(p => p.IdSanPham), // Mới nhất
				"price_oldest" => products.OrderBy(p => p.IdSanPham),      // Cũ nhất
				_ => products                                              // Không sắp xếp
			};

			// Thiết lập số lượng sản phẩm trên mỗi trang
			int pageSize = 3; // Hiển thị 6 sản phẩm mỗi trang
			var pagedProducts = await products.ToPagedListAsync(page, pageSize);

			// Lưu thông tin tìm kiếm vào ViewBag để hiển thị trên View
			ViewBag.Keyword = searchTerm;
			ViewBag.StartPrice = startprice;
			ViewBag.EndPrice = endprice;
			ViewBag.SortBy = sort_by;

			return View(pagedProducts); // Trả về danh sách sản phẩm phân trang
		}

		public async Task<IActionResult> Details(long IdSanPham)
		{
			if (IdSanPham < 0)
			{
				return RedirectToAction("Index");
			}
			var productsById = _dataContext.SanPhams.Where(p => p.IdSanPham == IdSanPham).FirstOrDefault();
			var relatedProducts = await _dataContext.SanPhams
			.Where(p => p.CategoriesId == productsById.CategoriesId && p.IdSanPham != productsById.IdSanPham).ToListAsync();
			ViewBag.RelatedProducts = relatedProducts;
			var companyProducts = await _dataContext.SanPhams
			.Where(p => p.BrandId == productsById.BrandId && p.IdSanPham != productsById.IdSanPham).ToListAsync();
			ViewBag.CompanyProducts = companyProducts;
			return View(productsById);
		}
	}
}
