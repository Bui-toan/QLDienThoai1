using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
using System.Diagnostics;
using X.PagedList;
namespace QLDienThoai.Controllers
{
	public class HomeController : Controller
	{
		private readonly QldienThoaiContext _dataContext;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, QldienThoaiContext context)
		{
			_logger = logger;
			_dataContext = context;
		}

		public IActionResult Index(int? page)
		{
			int pageSize = 6;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstsanpham = _dataContext.SanPhams.Include("Categories").Include("Brand").AsNoTracking().OrderBy(x => x.IdSanPham);
			PagedList<SanPham> products = new PagedList<SanPham>(lstsanpham, pageNumber, pageSize);
			return View(products);
			/*var products = _dataContext.SanPhams.Include("Categories").Include("Brand");
			return View(products);*/
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
