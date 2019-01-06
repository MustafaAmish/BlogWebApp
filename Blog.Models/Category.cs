using System.Collections.Generic;

namespace Blog.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<PostCategorys> PostCategoryses { get; set; } 
    }
}