using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
using X.PagedList;

namespace QLDienThoai.Controllers
{
	public class CategoryController : Controller
	{
		private readonly QldienThoaiContext _dataContext;

		public CategoryController(QldienThoaiContext context)
		{
			_dataContext = context;
		}


		public async Task<IActionResult> Index(string slug = "", string sort_by = "", int? page = 1, string startprice = "", string endprice = "")
		{
			// Kiểm tra xem slug có tồn tại không
			var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
			if (category == null)
			{
				return RedirectToAction("Index");
			}

			// Khởi tạo truy vấn IQueryable
			var query = _dataContext.SanPhams.Where(p => p.CategoriesId == category.CategoriesId);

			// Áp dụng bộ lọc giá nếu có
			if (!string.IsNullOrEmpty(startprice) && !string.IsNullOrEmpty(endprice))
			{
				if (decimal.TryParse(startprice, out var minPrice) && decimal.TryParse(endprice, out var maxPrice))
				{
					query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
				}
			}

			// Áp dụng sắp xếp
			query = sort_by switch
			{
				"price_increase" => query.OrderBy(p => p.Price),
				"price_decrease" => query.OrderByDescending(p => p.Price),
				"price_newest" => query.OrderByDescending(p => p.IdSanPham),
				"price_oldest" => query.OrderBy(p => p.IdSanPham),
				_ => query.OrderByDescending(p => p.IdSanPham)
			};

			// Thiết lập số lượng sản phẩm trên mỗi trang
			int pageSize = 6;
			int pageNumber = page ?? 1;
			ViewBag.CurrentSort = sort_by;
			ViewBag.Slug = slug;

			// Thực thi truy vấn và phân trang
			var productsByCategory = await query.ToPagedListAsync(pageNumber, pageSize);
			return View(productsByCategory);
		}



	}
}



