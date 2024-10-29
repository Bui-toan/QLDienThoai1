using System.ComponentModel.DataAnnotations;

namespace QLDienThoai.Models
{
	public class UserModel
	{
		public int UserId { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập UserName!")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập Email!"), EmailAddress]
		public string? Email { get; set; }

		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập Password!")]
		public string? Password { get; set; }
	}
}
