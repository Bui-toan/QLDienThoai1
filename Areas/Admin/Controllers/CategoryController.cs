using Microsoft.AspNetCore.Authorization;
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
				var slug = await _context.SanPhams.FirstOrDefaultAsync(p => p.Slug == category.Slug);
				if (slug != null)
				{
					ModelState.AddModelError("", "Sản phẩm đã có trong database");
					return View(category);
				}

				_context.Categories.Add(category);
				TempData["message"] = " Thêm sản phẩm thành công";
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Category");
			}
			return View(category);
		}
		[Route("Edit")]
		public async Task<IActionResult> Edit(int idCategogies)
		{
			Categories cate = await _context.Categories.FindAsync(idCategogies);
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

			Categories category = await _context.Categories.FindAsync(idCategories);
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			TempData["message"] = "Danh mục đã xóa";
			return RedirectToAction("Index", "Category");
		}

	}
}
