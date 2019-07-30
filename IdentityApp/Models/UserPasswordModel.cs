using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Models
{
    public class UserPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string OldPassword { get; set; }
    }
}
