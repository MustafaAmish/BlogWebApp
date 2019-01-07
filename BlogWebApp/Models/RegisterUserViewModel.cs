using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.Models
{
    public class RegisterUserViewModel
    {
        [Required()]
        [MinLength(3, ErrorMessage = "Must be 3 or more symbols!")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must be 3 or more symbols!")]
        public string LastName { get; set; }

        [Required()]
        [MinLength(10, ErrorMessage = "Must be 10 or more symbols!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Must be 6 or more symbols!")]
        public string Password { get; set; }

        [Required()]
        [MinLength(6, ErrorMessage = "Must be 6 or more symbols!")]
        public string ConfirmPassword { get; set; }
    }
}