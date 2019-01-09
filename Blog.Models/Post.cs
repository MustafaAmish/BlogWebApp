using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Blog.Models
{

    public class Post
    {
        public Post()
        {

            this.Comments = new List<Comment>();
            this.CreatedOn = DateTime.UtcNow;
            this.Categories = new List<PostCategorys>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Required minimum 10 symbols")]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

     
        public virtual ICollection<PostCategorys> Categories { get; set; }
        [Required]
        public string Genre { get; set; }

        public virtual List<Comment> Comments { get; set; }

    }
}

