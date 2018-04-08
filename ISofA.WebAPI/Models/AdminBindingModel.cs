using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISofA.WebAPI.Models
{
    public class AdminBindingModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public String Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public String Surname { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public String City { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public String PhoneNumber { get; set; }

        [Required]
        [Range(0, 1)]
        public int AdminType { get; set; }
    }
}