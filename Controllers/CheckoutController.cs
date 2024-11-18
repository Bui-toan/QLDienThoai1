using Microsoft.AspNetCore.Mvc;
using QLDienThoai.Models;
using QLDienThoai.Respository;
using System.Security.Claims;

namespace QLDienThoai.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly QldienThoaiContext _context;
		public CheckoutController(QldienThoaiContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var ordercode = Guid.NewGuid().ToString();
				var maxOrderId = _context.Orders.Max(o => (int?)o.OrderId) ?? 0; // Lấy OrderID lớn nhất hoặc gán 0 nếu không có bản ghi nào

				var orderItem = new Order();
				orderItem.OrderId = maxOrderId + 1; // Gán giá trị mới cho OrderID
				orderItem.UserName = userEmail;
				orderItem.OrderCode = ordercode;
				orderItem.Status = 1;
				orderItem.CreateDate = DateTime.Now;

				_context.Add(orderItem);
				_context.SaveChanges();
				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach (var item in cartItems)
				{
					var orderDetail= new OrderDetails();
					var maxOrderdetailId = _context.OrderDetails.Max(o => (int?)o.OrderDetailId) ?? 0;
					orderDetail.OrderDetailId = maxOrderdetailId + 1;
					orderDetail.Username = userEmail;
					orderDetail.ProductId=item.ProductId;
					orderDetail.Quantity = item.Quantity;
					orderDetail.Price = (decimal)item.Price;
					orderDetail.OrderCode=ordercode;
					_context.Add(orderDetail);
					_context.SaveChanges();

				}
				HttpContext.Session.Remove("Cart");
				TempData["success"] = "Checkout thành công, vui lòng chờ duyệt đơn hàng";
				return RedirectToAction("Index", "Cart");

			}
		}
	}
}
