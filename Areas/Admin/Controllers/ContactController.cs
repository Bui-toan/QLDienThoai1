using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using QLDienThoai.Models;

namespace QLDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    
    public class ContactController : Controller
    {
        private readonly QldienThoaiContext _context;
        public ContactController(QldienThoaiContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var contact = _context.Contacts.ToList();
            return View(contact);
        }
    }
}
