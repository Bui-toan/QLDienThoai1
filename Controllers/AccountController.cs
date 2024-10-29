using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using QLDienThoai.Models;
using QLDienThoai.Models.ViewModels;

namespace QLDienThoai.Controllers
{
    public class AccountController : Controller
    {
		private UserManager<AppUser> _userManager;
		private SignInManager<AppUser> _signInManager;

		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.PasswordHash, false, false);
                if (result.Succeeded)
                {
                    return Redirect(loginViewModel.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", "UserName hoặc Password không đúng!");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserModel user)
		{
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser { UserName = user.UserName, Email = user.Email };
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    TempData["success"] = "Tạo user thành công!";
                    return Redirect("/account/login");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
			return View(user);
		}
	}
}
