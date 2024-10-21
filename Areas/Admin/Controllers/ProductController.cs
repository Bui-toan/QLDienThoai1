using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;


namespace QLDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly QldienThoaiContext _context;
        public ProductController(QldienThoaiContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.SanPhams.OrderByDescending(p => p.IdSanPham).Include(p => p.Categories).Include(p => p.Brand).ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_context.Categories, "CategoriesId", "Name");
            return View();
        }

    }
}
