using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

namespace QLDienThoai.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Category")]
	//[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
	{
		private readonly QldienThoaiContext _context = new QldienThoaiContext();
		public CategoryController(QldienThoaiContext context)
		{
			_context = context;
		}
		public async Task<ActionResult> Index()
		{

			return View(await _context.Categories.OrderByDescending(x => x.CategoriesId).ToListAsync());
		}
		[HttpGet]
		[Route("Create")]
		public async Task<IActionResult> Create()
		{

			return View();
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Categories category)
		{
			if (ModelState.IsValid)
			{
				category.Slug = category.Name.Replace(" ", "-");
				var existingCategory = await _context.Categories
													 .FirstOrDefaultAsync(c => c.Name == category.Name);
				if (existingCategory != null)
				{
					ModelState.AddModelError("Name", "Danh mục với tên này đã tồn tại.");
					return View(category);
				}
				var maxCategoryId = _context.Categories.Max(o => (int?)o.CategoriesId) ?? 0;
				category.CategoriesId = maxCategoryId + 1;
				_context.Categories.Add(category);
				TempData["message"] = "Thêm danh mục thành công";
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Category");
			}
			return View(category);
		}

		[Route("Edit")]
		public async Task<IActionResult> Edit(int idCategories)
		{
			Categories cate = await _context.Categories.FindAsync(idCategories);
			return View(cate);
		}

		[HttpPost]
		[Route("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Categories category)
		{
			if (ModelState.IsValid)
			{
				category.Slug = category.Name.Replace(" ", "-");
				var slug = await _context.SanPhams.FirstOrDefaultAsync(p => p.Slug == category.Slug);
				if (slug != null)
				{
					ModelState.AddModelError("", "Sản phẩm đã có trong database");
					return View(category);
				}

				_context.Categories.Update(category);
				TempData["message"] = " Cập nhật danh mục thành công";
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Category");
			}
			return View(category);
		}
		[Route("Delete")]
		public async Task<IActionResult> Delete(int idCategories)
		{
			// Kiểm tra sản phẩm liên quan
			var relatedProducts = await _context.SanPhams.Where(sp => sp.CategoriesId == idCategories).ToListAsync();
			if (relatedProducts.Any())
			{
				TempData["message"] = "Không thể xóa danh mục vì còn sản phẩm liên quan.";
				return RedirectToAction("Index", "Category");
			}

			// Xóa danh mục nếu không có sản phẩm liên quan
			var category = await _context.Categories.FindAsync(idCategories);
			if (category == null)
			{
				TempData["message"] = "Danh mục không tồn tại.";
				return RedirectToAction("Index", "Category");
			}

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			TempData["message"] = "Danh mục đã xóa.";
			return RedirectToAction("Index", "Category");
		}


	}
}
