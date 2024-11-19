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

            // Lấy danh sách sản phẩm từ giỏ hàng
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            if (!cartItems.Any())
            {
                TempData["error"] = "Giỏ hàng của bạn đang trống. Vui lòng thêm sản phẩm vào giỏ hàng trước khi checkout.";
                return RedirectToAction("Index", "Cart");
            }

            // Tạo mã đơn hàng và đơn hàng mới
            var orderCode = Guid.NewGuid().ToString();
            var maxOrderId = _context.Orders.Max(o => (int?)o.OrderId) ?? 0;

            var orderItem = new Order
            {
                OrderId = maxOrderId + 1, // Tăng OrderId
                UserName = userEmail,
                OrderCode = orderCode,
                Status = 1,
                CreateDate = DateTime.Now
            };

            // Thêm đơn hàng vào context và lưu
            _context.Add(orderItem);
            await _context.SaveChangesAsync(); // Lưu trước để OrderId tồn tại trong bảng Orders

            // Lấy OrderDetailId lớn nhất hiện tại
            var maxOrderDetailId = _context.OrderDetails.Max(o => (int?)o.OrderDetailId) ?? 0;

            // Thêm chi tiết đơn hàng
            foreach (var item in cartItems)
            {
                maxOrderDetailId++; // Tăng OrderDetailId cho mỗi chi tiết đơn hàng

                var orderDetail = new OrderDetails
                {
                    OrderDetailId = maxOrderDetailId, // Gán ID tăng dần
                    OrderId = orderItem.OrderId,      // Liên kết với OrderId
                    Username = userEmail,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = (decimal)item.Price,
                    OrderCode = orderCode
                };

                _context.Add(orderDetail);
            }

            // Lưu tất cả thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Xóa giỏ hàng và thông báo thành công
            HttpContext.Session.Remove("Cart");
            TempData["success"] = "Checkout thành công, vui lòng chờ duyệt đơn hàng.";
            return RedirectToAction("Index", "Cart");
        }
    }
}
