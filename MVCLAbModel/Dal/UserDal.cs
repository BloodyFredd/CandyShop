using CandyShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CandyShop.Dal
{
    //This is where we get the list of all the users from the database
    public class UserDal:DbContext
    {
        public DbSet<User>  Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
        }

    }
}