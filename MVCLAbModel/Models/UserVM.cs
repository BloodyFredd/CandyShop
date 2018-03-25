using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CandyShop.Models
{
    public class UserVM
    {
        public User User { get; set; }
        public List<User> Users { get; set; }
    }
}