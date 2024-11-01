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

		public async Task<IActionResult> Index(string slug = "", string sort_by = "")
		{
			// Kiểm tra xem slug có tồn tại không
			var category = await _dataContext.Categories
				.FirstOrDefaultAsync(c => c.Slug == slug);

			if (category == null)
			{
				return RedirectToAction("Index");
			}

			// Khởi tạo truy vấn IQueryable
			var query = _dataContext.SanPhams
				.Where(p => p.CategoriesId == category.CategoriesId);

			// Áp dụng sắp xếp dựa trên tham số sort_by
			query = sort_by switch
			{
				"price_increase" => query.OrderBy(p => p.Price),
				"price_decrease" => query.OrderByDescending(p => p.Price),
				"price_newest" => query.OrderByDescending(p => p.IdSanPham),
				"price_oldest" => query.OrderBy(p => p.IdSanPham),
				_ => query.OrderByDescending(p => p.IdSanPham) // Mặc định sắp xếp theo Id giảm dần
			};

			// Thực thi truy vấn và tải dữ liệu
			var productsByCategory = await query.ToListAsync();

			return View(productsByCategory);
		}
	}
}


