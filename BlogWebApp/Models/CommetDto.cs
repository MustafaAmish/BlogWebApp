using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApp.Models
{
    public class CommetDto
    {
        [Required]
        [MinLength(10, ErrorMessage = "Min lenght 10 symbol!")]
        public string Content { get; set; }
    }
}
