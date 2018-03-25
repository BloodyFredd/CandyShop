using CandyShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CandyShop.Dal
{
    //This is where we get the list of all the orders from the database
    public class OrderDal : DbContext
        {

            public DbSet<Order> Orders { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Order>().ToTable("Orders");
            }

        }

}