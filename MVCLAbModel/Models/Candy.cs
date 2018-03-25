using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CandyShop.Models
{
    public class Candy
    {

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The candy name should be a-z and A-Z.")]
        [Required(ErrorMessage = "Candy name is required")]
        [Key]
        public string CandyName { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The candy type should a-z and A-Z.")]
        [Required(ErrorMessage = "Candy type is required")]
        public string CandyType { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The candy color should a-z and A-Z.")]
        [Required(ErrorMessage = "Candy color is required")]
        public string CandyColor { get; set; }

    }
}