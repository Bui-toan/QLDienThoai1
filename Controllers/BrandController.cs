using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
namespace QLDienThoai.Controllers
{
	public class BrandController : Controller
	{
		private readonly QldienThoaiContext _dataContext;
		public BrandController(QldienThoaiContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IActionResult> Index(String Slug = "")
		{
			Brand brand = _dataContext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();
			if (brand == null)
			{
				return RedirectToAction("Index");
			}
			var productsbyBrand = _dataContext.SanPhams.Where(p => p.BrandId == brand.BrandId);
			return View(await productsbyBrand.OrderByDescending(p => p.CategoriesId).ToListAsync());
		}
	}
}
