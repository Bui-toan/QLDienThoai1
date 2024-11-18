namespace QLDienThoai.Models
{
    public class ProductAPI
    {
        public int IdSanPham { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? BrandId { get; set; }
        public string? Images { get; set; }
        public int? CategoriesId { get; set; }
    }
}
