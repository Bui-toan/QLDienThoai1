using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

namespace QLDienThoai.Respository.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly QldienThoaiContext _context;
        public FooterViewComponent(QldienThoaiContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Contacts.FirstOrDefaultAsync());
    }
}
