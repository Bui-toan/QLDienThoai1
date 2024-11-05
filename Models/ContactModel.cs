using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDienThoai.Models
{
    public class ContactModel
    {
        
        public string? Name { get; set; }

        public string? Map {  get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Description { get; set; }

        
        public string? LogoImg { get; set; }

        [NotMapped]
        public IFormFile? ImageUpLoad { get; set; }

        public static implicit operator ContactModel?(Contact? v)
        {
            if (v == null) return null;

            return new ContactModel
            {
                Name = v.Name,
                Map = v.Map,
                Phone = v.Phone,
                Email = v.Email,
                Description = v.Description,
                LogoImg = v.LogoImg
                // Giả sử các thuộc tính này có mặt trong lớp Contact
            };
        }

    }
}
