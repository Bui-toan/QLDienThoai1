namespace QLDienThoai.Models
{
    public class ContactModel
    {
      
        public string? Name { get; set; }

        public string Map {  get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string LogiImg { get; set; }

        public IFormFile? ImageUpLoad { get; set; }
    }
}
