using Microsoft.AspNetCore.Identity;

namespace QLDienThoai.Models
{
	public class AppUser : IdentityUser
	{
		public string? Occupation { get; set; }

		public string? RoleId {  get; set; }
	}
}
