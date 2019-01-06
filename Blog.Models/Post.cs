using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{

    public class Post
    {
        public Post()
        {

            this.Comments = new List<Comment>();
            this.CreatedOn = DateTime.UtcNow;
            this.Categoryses = new List<PostCategorys>();
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

     
        public virtual ICollection<PostCategorys> Categoryses { get; set; }
        [Required]
        public string Genre { get; set; }

        public virtual List<Comment> Comments { get; set; }

    }
}

