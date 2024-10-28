
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;


namespace QLDienThoai.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Route("Product")]
	public class ProductController : Controller
	{
		private readonly QldienThoaiContext _context = new QldienThoaiContext();
		private readonly IWebHostEnvironment _environment;
		public ProductController(QldienThoaiContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_environment = webHostEnvironment;
		}


		public async Task<IActionResult> Index()

		{

			return View(await _context.SanPhams.OrderByDescending(p => p.IdSanPham).Include(p => p.Categories).Include(p => p.Brand).ToListAsync());
		}
		[HttpGet]
		[Route("Create")]
		public async Task<IActionResult> Create()
		{
			ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoriesId", "Name");
			ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "BrandId", "Name");
			return View();
		}

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create(SanPham product)
		{
			ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoriesId", "Name", product.CategoriesId);
			ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "BrandId", "Name", product.BrandId);

			if (ModelState.IsValid)
			{
				product.Slug = product.Name.Replace(" ", "-");
				var slug = await _context.SanPhams.FirstOrDefaultAsync(p => p.Slug == product.Slug);
				if (slug != null)
				{
					ModelState.AddModelError("", "Sản phẩm đã có trong database");
					return View(product);
				}
				else
				{
					if (product.ImageUpload != null)
					{
						string upLoadsDir = Path.Combine(_environment.WebRootPath, "images");
						string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
						string imagePath = Path.Combine(upLoadsDir, imageName);

						FileStream fs = new FileStream(imagePath, FileMode.Create);
						await product.ImageUpload.CopyToAsync(fs);
						fs.Close();
						product.Images = imageName;
					}
				}
				_context.SanPhams.Add(product);
				TempData["Success"] = " Thêm sản phẩm thành công";
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Product");
			}
			return View(product);
		}
	}
}
