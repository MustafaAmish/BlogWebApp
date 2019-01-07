using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.Models
{
    public class CommetDto
    {
        [Required]
        [MinLength(10, ErrorMessage = "Min lenght 10 symbol!")]
        public string Content { get; set; }
    }
}
