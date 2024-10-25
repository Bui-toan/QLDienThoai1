using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace QLDienThoai.Respository.Validation
{
	public class FileExtensionAttribute:ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if(value is IFormFile file)
			{
				var kieuimg= Path.GetExtension(file.FileName);
				string[] imgs = { "jpg", "png", "jpeg" };
				bool result = imgs.Any(x=>kieuimg.EndsWith(x));
				if(!result)
				{
					return new ValidationResult("Phải là kiểu img or png or jpeg");
				}

			}
			 return ValidationResult.Success; 
		}
	}
}
