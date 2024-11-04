
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDienThoai.Models
{
    public partial class Contact
    {

        public string? Name { get; set; }

        public string Map { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string LogoImg { get; set; }

        [NotMapped]
        public IFormFile ImageUpLoad { get; set; }

     

    }
}
