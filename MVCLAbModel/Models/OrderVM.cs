using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CandyShop.Models
{
    public class OrderVM
    {
            public Order Order { get; set; }
            public List<Order> Orders { get; set; }
    }
}