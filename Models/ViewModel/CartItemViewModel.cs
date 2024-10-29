namespace QLDienThoai.Models.ViewModel
{
	public class CartItemViewModel
	{
		public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
		public decimal? GrandTotal { get; set; }
	}
}
