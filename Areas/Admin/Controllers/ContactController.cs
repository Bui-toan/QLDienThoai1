using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using QLDienThoai.Models;

namespace QLDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    
    public class ContactController : Controller
    {
        private readonly QldienThoaiContext _context;
        private readonly IWebHostEnvironment _environment;
        public ContactController(QldienThoaiContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _environment = webHostEnvironment;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            var contact = _context.Contacts.ToList();
            return View(contact);
        }
        [Route("Edit")]
        public async Task<IActionResult> Edit()
        {
            ContactModel contact = await _context.Contacts.FirstOrDefaultAsync();
            return View(contact);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactModel contact)
        {            
            var existed_contact = _context.Contacts.FirstOrDefault();
            if (ModelState.IsValid)
            {                              
                    if (contact.ImageUpLoad != null)
                    {
                        string upLoadsDir = Path.Combine(_environment.WebRootPath, "images/404");
                        string imageName = Guid.NewGuid().ToString() + "_" + contact.ImageUpLoad.FileName;
                        string imagePath = Path.Combine(upLoadsDir, imageName);

                        FileStream fs = new FileStream(imagePath, FileMode.Create);
                        await contact.ImageUpLoad.CopyToAsync(fs);
                        fs.Close();
                        existed_contact.LogoImg = imageName;
                    }

                existed_contact.Name = contact.Name;
                existed_contact.Email = contact.Email;
                existed_contact.Phone = contact.Phone;
                existed_contact.Map = contact.Map;
                existed_contact.Description = contact.Description;


                _context.Update(existed_contact);
                TempData["success"] = " Cập nhật thông tin Web thành công";
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model có một vài thứ đang lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(contact);
        }
    }
}
