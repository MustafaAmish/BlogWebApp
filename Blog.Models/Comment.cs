using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
        public virtual  User User { get; set; }

        public int PostId { get; set; }
        public virtual  Post Post { get; set; }
    }
}
