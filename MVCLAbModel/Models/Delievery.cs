using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CandyShop.Models
{
    public class Delievery
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int delieveryID { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }

        [Required]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "The Phone should contain 9 digits only.")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The product name should a-z and A-Z.")]
        public string delieveryName { get; set; }
    }
}