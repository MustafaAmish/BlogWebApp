

using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            //this.Posts=new List<Post>();
        }
        
        public string FistName { get; set; }

        public string LastName { get; set; }

        //public virtual ICollection<Post> Posts { get; set; }

        // public ICollection<GenreType> Interestet { get; set; }

    }
}
