using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
using System.Diagnostics;

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

		public IActionResult Index()
		{
			var products = _dataContext.SanPhams.Include("Categories").Include("Brand").ToList();
			return View(products);
		}

		public IActionResult Privacy()
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
