using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CandyShop.Models
{
    public class CandyVM
    {

        public Candy Candy { get; set; }
        public List<Candy> Candies { get; set; }

    }
}