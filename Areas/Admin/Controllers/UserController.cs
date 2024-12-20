﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;
using QLDienThoai.Models.ViewModels;

namespace QLDienThoai.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/User")]
	[Authorize(Roles = "Admin")]

	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var users = await _userManager.Users.ToListAsync();
			var userViewModels = new List<UserViewModel>();

			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				var roleName = roles.FirstOrDefault();

				if (roleName == null) roleName = "No Role";

				userViewModels.Add(new UserViewModel
				{
					Id = user.Id,
					UserName = user.UserName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					RoleName = roleName
				});
			}

			return View(userViewModels);
		}

		[HttpGet]
		[Route("Create")]
		public async Task<IActionResult> Create()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");
			return View(new AppUser());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Create")]
		public async Task<IActionResult> Create(AppUser user)
		{
			if (ModelState.IsValid)
			{
				var createUserResult = await _userManager.CreateAsync(user, user.PasswordHash);
				if (createUserResult.Succeeded)
				{
					var createUser = await _userManager.FindByEmailAsync(user.Email);
					var userId = createUser.Id;
					var role = await _roleManager.FindByIdAsync(user.RoleId);

					if (role != null)
					{
						var addToRoleResult = await _userManager.AddToRoleAsync(createUser, role.Name);
						if (!addToRoleResult.Succeeded)
						{
							foreach (var error in addToRoleResult.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
						}
						else
						{
							TempData["success"] = "Tạo user và chọn role thành công!";
							return RedirectToAction("Index", "User");
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Role không tồn tại.");
					}
				}
				else
				{
					AddIdentityErrors(createUserResult);
				}
			}
			else
			{
				TempData["error"] = "Model có lỗi!";
				List<string> errors = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach (var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}
				string errorMessage = string.Join("\n", errors);
				return BadRequest(errorMessage);
			}

			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");
			return View(user);
		}


		private void AddIdentityErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		[HttpGet]
		[Route("Delete")]
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}

			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			// Xóa các roles của user trước khi xóa user
			var roles = await _userManager.GetRolesAsync(user);
			if (roles.Any())
			{
				var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, roles);
				if (!removeRolesResult.Succeeded)
				{
					foreach (var error in removeRolesResult.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View("Error");
				}
			}

			// Xóa user
			var deleteResult = await _userManager.DeleteAsync(user);
			if (!deleteResult.Succeeded)
			{
				foreach (var error in deleteResult.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return View("Error");
			}

			TempData["success"] = "Xóa user thành công!";
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("Edit")]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");

			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Edit")]
		public async Task<IActionResult> Edit(string id, AppUser user)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var existingUser = await _userManager.FindByIdAsync(id);
				if (existingUser == null)
				{
					return NotFound();
				}

				existingUser.UserName = user.UserName;
				existingUser.Email = user.Email;
				existingUser.PhoneNumber = user.PhoneNumber;

				var currentRoles = await _userManager.GetRolesAsync(existingUser);

				if (currentRoles.Any())
				{
					var removeRolesResult = await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
					if (!removeRolesResult.Succeeded)
					{
						ModelState.AddModelError("", "Có lỗi xảy ra khi xóa vai trò hiện tại của user.");
						return View(user);
					}
				}

				var newRole = await _roleManager.FindByIdAsync(user.RoleId);
				if (newRole != null)
				{
					var addToRoleResult = await _userManager.AddToRoleAsync(existingUser, newRole.Name);
					if (!addToRoleResult.Succeeded)
					{
						ModelState.AddModelError("", "Có lỗi xảy ra khi thêm vai trò mới cho user.");
						return View(user);
					}
				}
				else
				{
					ModelState.AddModelError("", "Vai trò mới không tồn tại.");
					return View(user);
				}

				var updateResult = await _userManager.UpdateAsync(existingUser);
				if (updateResult.Succeeded)
				{
					TempData["success"] = "Sửa user thành công!";
					return RedirectToAction("Index");
				}
				else
				{
					ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin user.");
				}
			}

			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");
			return View(user);
		}
	}
}
