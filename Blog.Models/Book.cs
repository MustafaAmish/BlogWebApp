using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
  public  class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MinLength(100,ErrorMessage = "Minimum of 100 characters!!!")]
        public string Content { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public string Uri { get; set; }
    }
}
