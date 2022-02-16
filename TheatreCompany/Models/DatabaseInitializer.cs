using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace TheatreCompany.Models
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<TheatreCompanyDbContext>
    {
        // This method will seed the database
        protected override void Seed(TheatreCompanyDbContext context)
        {
            if (!context.Users.Any())
            {
                //============================================================
                // Create a few roles and store them in the AspNetRoles tables
                //============================================================

                // Create a roleManager object that will allow us to create the roles and store them in the database
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // If the Admin role doesnt exist...
                if (!roleManager.RoleExists("Admin"))
                {
                    // then we create an Admin Role
                    roleManager.Create(new IdentityRole("Admin"));
                }

                // If the member roles doesnt exist...
                if (!roleManager.RoleExists("Member"))
                {
                    // then we create a Member role
                    roleManager.Create(new IdentityRole("Member"));
                }

                // Save the new roles to the database
                context.SaveChanges();

                //=====================================================
                // Create some users and assign them to different roles
                //=====================================================

                // The userManager object allows us to create uysers and store them in the database
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                // If the users with the admin@theatrecompany.com username doesnt exist...
                if(userManager.FindByName("admin@theatrecompany.com") == null)
                {
                    // we use a super relaxed password validator
                    userManager.PasswordValidator = new PasswordValidator()
                    {
                        RequiredLength = 1,
                        RequireNonLetterOrDigit = false,
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                    };

                    // then we create a new user admin
                    var admin = new User()
                    {
                        UserName = "admin@theatrecompany.com",
                        Email = "admin@theatrecompany",
                        FirstName = "Jim",
                        LastName = "Smith",
                        Street = "56 High Street",
                        City = "Glasgow",
                        PostCode = "G1 67AD",
                        EmailConfirmed = true,
                        PhoneNumber = "00447869145567"
                    };

                    // add the hashed password to user
                    userManager.Create(admin, "admin123");

                    // add the user to the role admin
                    userManager.AddToRole(admin.Id, "Admin");

                    //====================
                    //create a few members
                    //====================

                    // Member1
                    var member1 = new User()
                    {
                        UserName = "member1@gmail.com",
                        Email = "member1@gmail.com",
                        FirstName = "Paul",
                        LastName = "Goat",
                        Street = "5 Merry Street",
                        City = "Coatbridge",
                        PostCode = "ML1 67AD",
                        EmailConfirmed = true,
                        PhoneNumber = "90447979164499"
                    };

                    if (userManager.FindByName("member1@gmail.com") == null)
                    {
                        userManager.Create(member1, "password1");
                        userManager.AddToRole(member1.Id, "Member");
                    }

                    // Member2
                    var member2 = new User()
                    {
                        UserName = "member2@yahoo.com",
                        Email = "member2@yahoo.com",
                        FirstName = "Luigi",
                        LastName = "Musolini",
                        Street = "15 Confused Street",
                        City = "Rutherglen",
                        PostCode = "61 7H0",
                        EmailConfirmed = true,
                        PhoneNumber = "00447779163399"
                    };

                    if (userManager.FindByName("member2@yahoo.com") == null)
                    {
                        userManager.Create(member2, "password2");
                        userManager.AddToRole(member2.Id, "Member");
                    }


                    // save users to the database
                    context.SaveChanges();


                    //=============================
                    // Seeding the Categories Table
                    //=============================

                    //create a few categories
                    var cat1 = new Category() { Name = "Motors" };
                    var cat2 = new Category() { Name = "Property" };
                    var cat3 = new Category() { Name = "Jobs" };
                    var cat4 = new Category() { Name = "Services" };
                    var cat5 = new Category() { Name = "Pets" };
                    var cat6 = new Category() { Name = "For Sale" };

                    //add each category to the Categories table
                    context.Categories.Add(cat1);
                    context.Categories.Add(cat2);
                    context.Categories.Add(cat3);
                    context.Categories.Add(cat4);
                    context.Categories.Add(cat5);

                    // save the changes to the database
                    context.SaveChanges();


                    //========================
                    // Seeding the Posts Table
                    //========================

                    //create a post
                    var post1 = new Post()
                    {
                        Title = "House For Sale",
                        Description = "Beautiful 5 bedroom detached house",
                        Location = "Glasgow",
                        Price = 145000m,
                        DatePosted = new DateTime(2019, 1, 1, 8, 0, 15), //this is the date when the post/ad was created
                        DateExpired = new DateTime(2019, 1, 1, 8, 0, 15).AddDays(14), //the post will expire after 14 days
                        User = member2,
                        Category = cat2
                    };

                    //add the post to the posts table
                    context.Posts.Add(post1);


                    var post2 = new Post()
                    {
                        Title = "Hyunday Tucson",
                        Description = "Beautiful 2016 Hyunday 5Dr",
                        Location = "Edinburgh",
                        Price = 14000m,
                        DatePosted = new DateTime(2019, 5, 25, 8, 0, 15),
                        DateExpired = new DateTime(2019, 5, 25, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat1
                    };
                    context.Posts.Add(post2);


                    var post3 = new Post()
                    {
                        Title = "Audi Q5",
                        Description = "Beautiful 2019 Audi Q5",
                        Location = "Aberdeen",
                        Price = 56000m,
                        DatePosted = new DateTime(2019, 1, 25, 6, 0, 15),
                        DateExpired = new DateTime(2019, 1, 25, 6, 0, 15).AddDays(14),
                        User = member1,
                        Category = cat1
                    };
                    context.Posts.Add(post3);


                    var post4 = new Post()
                    {
                        Title = "Lhasso Apso",
                        Description = "Beautiful 2 years old Lhasso Apso",
                        Location = "Galsgow",
                        Price = 500m,
                        DatePosted = new DateTime(2019, 3, 5, 8, 0, 15),
                        DateExpired = new DateTime(2019, 3, 5, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat5
                    };
                    context.Posts.Add(post4);


                    var post5 = new Post()
                    {
                        Title = "Mercedes Benz A180",
                        Description = "Beautiful 2018 Mercedes Benz class A180",
                        Location = "Edinburgh",
                        Price = 34000m,
                        DatePosted = new DateTime(2019, 4, 5, 5, 0, 15),
                        DateExpired = new DateTime(2019, 4, 5, 5, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat1
                    };
                    context.Posts.Add(post5);


                    var post6 = new Post()
                    {
                        Title = "Hyunday Tucson",
                        Description = "Beautiful 2017 Hyunday 5Dr",
                        Location = "Edinburgh",
                        Price = 14000m,
                        DatePosted = new DateTime(2019, 4, 5, 5, 0, 15),
                        DateExpired = new DateTime(2019, 4, 5, 5, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat1
                    };
                    context.Posts.Add(post6);

                    // save the changes to the database
                    context.SaveChanges();

                    // Add Comments
                    var Comment1p1 = new Comment()
                    {
                        Description = "too many Doors",
                        DatePosted = new DateTime(2019, 4, 5, 5, 0, 15),
                        DateExpired = new DateTime(2019, 4, 5, 5, 0, 15).AddDays(14),
                        User = member2,
                        Post = post6
                    };
                    context.Comments.Add(Comment1p1);

                    // save the changes to the database
                    context.SaveChanges();
                }// end if
            }// end if
        }// end seed method
    }// end class
}// end namespace