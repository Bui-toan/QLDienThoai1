using Microsoft.AspNetCore.Mvc;

namespace QLDT.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
