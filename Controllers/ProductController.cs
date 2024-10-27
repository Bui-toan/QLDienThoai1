using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

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
		public async Task<IActionResult> Search(string searchTerm)
		{
			var products = await _dataContext.SanPhams.
				Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm)).
				ToListAsync();
			ViewBag.Keyword = searchTerm;
			return View(products);
			;
		}
		public async Task<IActionResult> Details(long IdSanPham)
		{
			if (IdSanPham < 0)
			{
				return RedirectToAction("Index");
			}
			var productsById = _dataContext.SanPhams.Where(p => p.IdSanPham == IdSanPham).FirstOrDefault();
			var relatedProducts = await _dataContext.SanPhams
			.Where(p => p.CategoriesId == productsById.CategoriesId && p.IdSanPham != productsById.IdSanPham).
			 Take(4).ToListAsync();
			ViewBag.RelatedProducts = relatedProducts;
			return View(productsById);
		}
	}
}
