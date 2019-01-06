using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models
{
  public  class PostCategorys
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
