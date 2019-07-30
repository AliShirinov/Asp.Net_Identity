using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Models.ViewModels
{
    public class RegisterModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(maximumLength:14,MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 14, MinimumLength = 3)]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
