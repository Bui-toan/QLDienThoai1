using System.ComponentModel.DataAnnotations;

namespace QLDienThoai.Models.ViewModels
{
	public class LoginViewModel
	{
		public int UserId { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập UserName!")]
		public string? UserName { get; set; }

		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập Password!")]
		public string? PasswordHash { get; set; }

		public string? ReturnUrl { get; set; }
	}
}
