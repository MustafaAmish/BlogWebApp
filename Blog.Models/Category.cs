using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }

        public virtual ICollection<PostCategorys> PostCategoryses { get; set; } 
    }
}