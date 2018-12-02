using System;
using System.Collections.Generic;
using System.Text;
using Blog.Models.Enums;

namespace Blog.Models
{
  
        public class Post
        {
            public Post()
            {
               
                this.Comments = new List<Comment>();
            }

            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public DateTime CreatedOn => DateTime.UtcNow;

            public GenreType Genre { get; set; }

            public virtual List<Comment> Comments { get; set; }

        }
    }

