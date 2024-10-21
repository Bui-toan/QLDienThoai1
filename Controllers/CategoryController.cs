using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
namespace QLDienThoai.Controllers
{
	public class CategoryController : Controller
	{
		private readonly QldienThoaiContext _dataContext;
		public CategoryController(QldienThoaiContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(String Slug = "")
		{
			Categories category = _dataContext.Categories.Where(c => c.Slug == Slug).FirstOrDefault();
			if (category == null)
			{
				return RedirectToAction("Index");
			}
			var productsbyCategory = _dataContext.SanPhams.Where(p => p.CategoriesId == category.CategoriesId);
			return View(await productsbyCategory.OrderByDescending(p => p.CategoriesId).ToListAsync());
		}

	}
}
