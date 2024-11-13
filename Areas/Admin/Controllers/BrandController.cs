using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

namespace QLDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brands")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly QldienThoaiContext _context = new QldienThoaiContext();
        public BrandController(QldienThoaiContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {

            return View(await _context.Brands.OrderByDescending(x => x.BrandId).ToListAsync());
        }
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brands brand)
        {
            if (ModelState.IsValid)
            {
                brand.Slug = brand.Name.Replace(" ", "-");
                var slug = await _context.SanPhams.FirstOrDefaultAsync(p => p.Slug == brand.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã có trong database");
                    return View(brand);
                }

                _context.Brands.Add(brand);
                TempData["message"] = " Thêm sản phẩm thành công";
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Brand");
            }
            return View(brand);
        }
        [Route("Edit")]
        public async Task<IActionResult> Edit(int idBrand)
        {
            Brands brand = await _context.Brands.FindAsync(idBrand);
            return View(brand);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brands idBrand)
        {
            if (ModelState.IsValid)
            {
                idBrand.Slug = idBrand.Name.Replace(" ", "-");
                var slug = await _context.SanPhams.FirstOrDefaultAsync(p => p.Slug == idBrand.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã có trong database");
                    return View(idBrand);
                }

                _context.Brands.Update(idBrand);
                TempData["message"] = " Cập nhật danh mục thành công";
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Brand");
            }
            return View(idBrand);
        }
        [Route("Delete")]
        public async Task<IActionResult> Delete(int idBrand)
        {

            Brands brand = await _context.Brands.FindAsync(idBrand);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            TempData["message"] = "Danh mục đã xóa";
            return RedirectToAction("Index", "Brand");
        }

    }
}
