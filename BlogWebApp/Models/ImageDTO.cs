using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApp.Models
{
    public class ImageDTO
    {
        [Required]
       public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public byte[] Img { get; set; }

    }
}
