using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class User : IdentityUser<string>
    {
        public string FistName { get; set; }

        public string LastName { get; set; }
    }
}
