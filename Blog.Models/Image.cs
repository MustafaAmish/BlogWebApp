using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(100,ErrorMessage = "Minimus symbol 100!!!")]
        public string Description { get; set; }
        [Required]
        public byte[] Img { get; set; }
        //TODO requared to all
    }
}
