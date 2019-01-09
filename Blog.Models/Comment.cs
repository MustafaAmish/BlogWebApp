using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace Blog.Models
{
    public class Comment
    {
        public Comment()
        {
            this.CreateOn=DateTime.UtcNow;
        }

        public int Id { get; set; }
        [Required]
        public string Content { get; set; }

        public DateTime CreateOn { get; set; }
        
        public string UserId { get; set; }
        [Required]
        public virtual  User User { get; set; }

        public int PostId { get; set; }
        [Required]
        public virtual  Post Post { get; set; }
    }
}
