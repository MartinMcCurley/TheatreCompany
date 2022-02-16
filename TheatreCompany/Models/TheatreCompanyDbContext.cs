using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreCompany.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

// This is a newly created file to split the original Identity Models file into two
namespace TheatreCompany.Models
{
    public class TheatreCompanyDbContext : IdentityDbContext<User>
    {
        // These will create Categories and Posts tables in the db when app runs
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        public TheatreCompanyDbContext()
            : base("TheatreCompanyConnection2", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public static TheatreCompanyDbContext Create()
        {
            return new TheatreCompanyDbContext();
        }
    }
}