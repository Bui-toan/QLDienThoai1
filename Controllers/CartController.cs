using Microsoft.AspNetCore.Mvc;
using QLDienThoai.Models;
using QLDienThoai.Models.ViewModel;
using QLDienThoai.Respository;

namespace QLDienThoai.Controllers
{
	public class CartController : Controller
	{
		private readonly QldienThoaiContext _dataContext;
		public CartController(QldienThoaiContext _context)
		{
			_dataContext = _context;
		}
		public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
			};
			return View(cartVM);
		}
		public IActionResult Checkout()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}


		public async Task<IActionResult> Add(int IdSanPham)
		{
			SanPham product = await _dataContext.SanPhams.FindAsync(IdSanPham);
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItem = cart.Where(c => c.ProductId == IdSanPham).FirstOrDefault();
			if (cartItem == null)
			{
				cart.Add(new CartItemModel(product));
			}
			else
			{
				cartItem.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", cart);
			TempData["success"] = "Thêm sản phẩm thành công";
			return Redirect(Request.Headers["Referer"].ToString());
		}

		[HttpPost]
		public async Task<IActionResult> Decrease(int ProductId)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(c => c.ProductId == ProductId).FirstOrDefault();
			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == ProductId);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

			/*TempData["success"] = "Giảm số lượng sản phẩm thành công";*/
			return PartialView("_CartPartial", GetCartViewModel(cart));
		}

		[HttpPost]
		public async Task<IActionResult> Inscrease(int ProductId)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(c => c.ProductId == ProductId).FirstOrDefault();
			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == ProductId);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			/*TempData["success"] = "Tăng số lượng sản phẩm thành công";*/
			return PartialView("_CartPartial", GetCartViewModel(cart));
		}

		[HttpPost]
		public async Task<IActionResult> Remove(int ProductId)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			cart.RemoveAll(p => p.ProductId == ProductId);
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			/*		TempData["success"] = "Xóa sản phẩm thành công";*/
			return PartialView("_CartPartial", GetCartViewModel(cart));
		}

		[HttpPost]
		public async Task<IActionResult> Clear()
		{
			HttpContext.Session.Remove("Cart");
			/*TempData["successMessage"] = "Đã xóa tất cả sản phẩm khỏi giỏ hàng";*/
			return PartialView("_CartPartial", new CartItemViewModel());
		}

		private CartItemViewModel GetCartViewModel(List<CartItemModel> cartItems)
		{
			return new CartItemViewModel
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
			};
		}



	}
}
