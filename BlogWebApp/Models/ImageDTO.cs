using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.Models
{
    public class ImageDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public byte[] Img { get; set; }

    }
}
