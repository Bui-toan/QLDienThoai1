using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Details(int IdSanPham)
        {
            if (IdSanPham < 0)
            {
                return RedirectToAction("Index");
            }
            var productsById = _dataContext.SanPhams.Where(p => p.IdSanPham == IdSanPham).FirstOrDefault();

            return View(productsById);
        }
    }
}
