using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLDienThoai.Models;

public partial class Users
{
	public int UserId { get; set; }

	public string? Occupation { get; set; }

	[Required(ErrorMessage = "Vui lòng nhập UserName!")]
	public string? UserName { get; set; }

	public string? NormalizedUserName { get; set; }

	[Required(ErrorMessage = "Vui lòng nhập Email!"), EmailAddress]
	public string? Email { get; set; }

	public string? NormalizedEmail { get; set; }

	public bool? EmailConfirmed { get; set; }

	[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập Password!")]
	public string? PasswordHash { get; set; }

	public string? SecurityStamp { get; set; }

	public string? PhoneNumber { get; set; }

	public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

	public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

	public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

	public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();

	public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
