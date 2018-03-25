using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CandyShop.Models
{
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The order name should only be a-z or A-Z.")]
        [Required(ErrorMessage = "Order name is required")]
        public string OrderName { get; set; }

        [Required(ErrorMessage = "Order amount is required")]
        [RegularExpression(@"^([1-9][0-9]{0,2}|1000)$", ErrorMessage = "The order amount should be between 1-1000.")]
        public string orderAmount { get; set; }

    }
}