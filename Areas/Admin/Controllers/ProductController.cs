﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
using X.PagedList;


namespace QLDienThoai.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin")]
	[Authorize(Roles = "Admin")]

	public class ProductController : Controller
	{
		private readonly QldienThoaiContext _context = new QldienThoaiContext();
		private readonly IWebHostEnvironment _environment;
		public ProductController(QldienThoaiContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_environment = webHostEnvironment;
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> Index(int? page)
		{
			int pageSize = 6;  // Số sản phẩm mỗi trang
			int pageNumber = page ?? 1;  // Mặc định trang 1 nếu page null

			// Lấy danh sách sản phẩm, bao gồm Categories và Brand
			var lstsanpham = _context.SanPhams
				.OrderByDescending(p => p.IdSanPham)  // Sắp xếp theo IdSanPham giảm dần
				.Include(p => p.Categories)
				.Include(p => p.Brand)
				.AsNoTracking();  // Không theo dõi đối tượng để tối ưu hiệu suất

			// Lấy sản phẩm cho trang hiện tại và phân trang
			var pagedProducts = await lstsanpham
				.ToPagedListAsync(pageNumber, pageSize);  // Sử dụng ToPagedListAsync để phân trang

			return View(pagedProducts);
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
						string imageName = product.IdSanPham.ToString() + ".jpg";
						string imagePath = Path.Combine(upLoadsDir, imageName);

						FileStream fs = new FileStream(imagePath, FileMode.Create);
						await product.ImageUpload.CopyToAsync(fs);
						fs.Close();
						product.Images = imageName;
					}
				}
				_context.SanPhams.Add(product);
				TempData["message"] = " Thêm sản phẩm thành công";
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Product");
			}
			return View(product);
		}
		[HttpGet]
		[Route("Edit")]
		public async Task<IActionResult> Edit(int idSanPham)
		{
			SanPham product = await _context.SanPhams.FindAsync(idSanPham);
			ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoriesId", "Name", product.CategoriesId);
			ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "BrandId", "Name", product.BrandId);
			return View(product);
		}
		[HttpPost]
		[Route("Edit")]
		public async Task<IActionResult> Edit(SanPham product)
		{
			ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoriesId", "Name", product.CategoriesId);
			ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "BrandId", "Name", product.BrandId);
			var oldproduct = _context.SanPhams.Find(product.IdSanPham);
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
						string imageName = product.IdSanPham.ToString() + ".jpg";
						string imagePath = Path.Combine(upLoadsDir, imageName);

						FileStream fs = new FileStream(imagePath, FileMode.Create);
						await product.ImageUpload.CopyToAsync(fs);
						fs.Close();
						oldproduct.Images = imageName;
					}
				}
				oldproduct.Name = product.Name;
				oldproduct.Price = product.Price;
				oldproduct.BrandId = product.BrandId;
				oldproduct.CategoriesId = product.CategoriesId;
				oldproduct.Description = product.Description;


				_context.SanPhams.Update(oldproduct);
				TempData["message"] = " Cập nhật sản phẩm thành công";
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Product");
			}
			return View(product);
		}

		[Route("Delete/{idSanPham:int}")]
		public async Task<IActionResult> Delete(int idSanPham)
		{
			// Tìm sản phẩm trong cơ sở dữ liệu
			var product = await _context.SanPhams.FindAsync(idSanPham);
			if (product == null)
			{
				TempData["error"] = "Không tìm thấy sản phẩm!";
				return RedirectToAction("Index");
			}

			try
			{
				// Kiểm tra và xóa các bản ghi liên quan trong OrderDetails
				var relatedOrders = _context.OrderDetails.Where(o => o.ProductId == idSanPham);
				if (relatedOrders.Any())
				{
					_context.OrderDetails.RemoveRange(relatedOrders);
					await _context.SaveChangesAsync(); // Lưu thay đổi khi xóa dữ liệu liên quan
				}

				// Kiểm tra và xóa file ảnh nếu cần
				if (!string.Equals(product.Images, "noname.jpg", StringComparison.OrdinalIgnoreCase))
				{
					string uploadsDir = Path.Combine(_environment.WebRootPath, "images");
					string imagePath = Path.Combine(uploadsDir, product.Images);

					if (System.IO.File.Exists(imagePath))
					{
						System.IO.File.Delete(imagePath);
					}
				}

				// Xóa sản phẩm
				_context.SanPhams.Remove(product);
				await _context.SaveChangesAsync();

				TempData["message"] = "Sản phẩm đã được xóa thành công.";
			}
			catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
			{
				// Lỗi vi phạm ràng buộc khóa ngoại
				TempData["error"] = "Không thể xóa sản phẩm vì có dữ liệu liên quan trong hệ thống.";
			}
			catch (Exception ex)
			{
				TempData["error"] = "Đã xảy ra lỗi khi xóa sản phẩm. Vui lòng thử lại.";
			}

			return RedirectToAction("Index");
		}


	}
}
