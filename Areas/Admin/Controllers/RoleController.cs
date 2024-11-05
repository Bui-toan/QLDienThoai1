using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QLDienThoai.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Role")]
	[Authorize(Roles = "Admin")]

	public class RoleController : Controller
	{
		//private readonly QldienThoaiContext db;
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _roleManager.Roles.OrderByDescending(p => p.Id).ToListAsync());
		}

		[HttpGet]
		[Route("Create")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Create")]
		public async Task<IActionResult> Create(IdentityRole role)
		{
			if (!_roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
				TempData["success"] = "Tạo role thành công!";
			}
			return Redirect("Index");
		}

		[HttpGet]
		[Route("Delete")]
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}

			try
			{
				await _roleManager.DeleteAsync(role);
				TempData["success"] = "Xóa role thành công!";
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Có lỗi xảy ra khi xóa role!");
			}

			return Redirect("Index");
		}

		[HttpGet]
		[Route("Edit")]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				return NotFound();
			}

			return View(role);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Edit")]
		public async Task<IActionResult> Edit(string id, IdentityRole role)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var newRole = await _roleManager.FindByIdAsync(id);
				if (newRole == null)
				{
					return NotFound();
				}
				newRole.Name = role.Name;

				try
				{
					await _roleManager.UpdateAsync(newRole);
					TempData["success"] = "Sửa role thành công!";
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", "Có lỗi xảy ra khi sửa role!");
				}
			}
			return View(role ?? new IdentityRole { Id = id });
		}
	}
}
