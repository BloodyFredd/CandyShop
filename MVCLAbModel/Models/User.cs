using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CandyShop.Models
{
    public class User
    {
        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 15 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 15 characters.")]
        public string LastName { get; set; }

        [Key]
        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "The Id should contain 4 digits only.")]
        public string ID { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        public int Manager { get; set; }
    }
}