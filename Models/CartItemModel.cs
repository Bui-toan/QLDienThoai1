namespace QLDienThoai.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total
        {
            get { return Quantity * Price; }
        }
        public string Image { get; set; }
        public CartItemModel()
        {

        }
        public CartItemModel(SanPham product)
        {
            ProductId = product.IdSanPham;
            ProductName = product.Name;
            Price = product.Price;
            Quantity = 1;
            Image = product.Images;
        }
    }
}
