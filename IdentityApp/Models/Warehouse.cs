using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:20,MinimumLength =2)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Address { get; set; }
    }
}
