using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blog.Models;
using Blog.Services.CustomAttributes;

namespace BlogWebApp.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(10,ErrorMessage = "Required minimum 10 symbols")]
        [NotNullOrWhiteSpace]
        public string Title { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "Required minimum 100 symbols")]
        [NotNullOrWhiteSpace]
        public string Description { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Required minimum 2 symbols")]
        [NotNullOrWhiteSpace]
        public string Genre { get; set; }

        public ICollection<PostCategorys> Categories { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}