using System.Collections.Generic;
using Blog.Models;

namespace BlogWebApp.Models
{
    public class PostAndCategoryDto
    {
        public ICollection<Post> Posts { get; set; }

        public ICollection<string> Categories { get; set; }   
    }
}