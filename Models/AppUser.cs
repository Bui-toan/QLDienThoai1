using Microsoft.AspNetCore.Identity;

namespace QLDienThoai.Models
{
	public class AppUser : IdentityUser
	{
		public string? FullName { get; set; }

		public string? Address { get; set; }

		public string? Occupation { get; set; }

		public string? RoleId {  get; set; }
	}
}
