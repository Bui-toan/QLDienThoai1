using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDienThoai.Models;


namespace QLDienThoai.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QldienThoaiContext db = new QldienThoaiContext();

        [HttpGet]
        public IActionResult Index()
        {
            // Trả về view để hiển thị form và dữ liệu sản phẩm
            var sanpham = (from p in db.SanPhams
                           select new ProductAPI
                           {
                               IdSanPham = p.IdSanPham,
                               Name = p.Name,
                               Description = p.Description,
                               Price = p.Price,
                               BrandId = p.BrandId,
                               Images = p.Images,
                               CategoriesId = p.CategoriesId
                           }).ToList();
            return Ok(sanpham);
        }
        [HttpPost]
        public IActionResult AddProduct([FromForm] ProductAPI product, [FromForm] IFormFile ImageUpload)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new SanPham
                {
                    IdSanPham = product.IdSanPham,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    BrandId = product.BrandId,
                    CategoriesId = product.CategoriesId
                };
                if (ImageUpload != null)
                { // Đảm bảo thư mục tồn tại
                  var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"); 
                   if (!Directory.Exists(uploadDirectory)) { Directory.CreateDirectory(uploadDirectory); }
                    var filePath = Path.Combine(uploadDirectory, ImageUpload.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create)) { ImageUpload.CopyTo(stream); }
                    newProduct.Images = "/images/" + ImageUpload.FileName; 
                }
                db.SanPhams.Add(newProduct);
                db.SaveChanges();
                return Ok(newProduct);
            }
            return BadRequest("Invalid product data");
        }
    }
}
