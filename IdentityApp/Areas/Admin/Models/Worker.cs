using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Areas.Admin.Models
{
    public class Worker
    {
        [Required]
        [StringLength(maximumLength:12,MinimumLength =3)]      
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string FatherName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string CurrentAddress { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public byte PassportNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirePassDate { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
