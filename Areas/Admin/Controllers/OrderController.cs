using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

namespace QLDienThoai.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Order")]
	//[Authorize(Roles = "Admin")]
	public class OrderController : Controller
	{
		private readonly QldienThoaiContext _context = new QldienThoaiContext();
		public OrderController (QldienThoaiContext context)
		{
			_context = context;
		}

		public async Task<ActionResult> Index()
		{

			return View(await _context.Orders.OrderByDescending(x => x.OrderId).ToListAsync());
		}
		[Route("ViewOrder")]
		public async Task<ActionResult> ViewOrder(string ordercode)
		{
			var detail= await _context.OrderDetails.Include(X => X.Product).Where(x=>x.OrderCode==ordercode).ToListAsync();
			return View(detail);
		}
	}
}
