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

                // If the member roles doesnt exist...
                if (!roleManager.RoleExists("Suspended"))
                {
                    // then we create a Member role
                    roleManager.Create(new IdentityRole("Suspended"));
                }

                // Save the new roles to the database
                context.SaveChanges();

                //=====================================================
                // Create some users and assign them to different roles
                //=====================================================

                // The userManager object allows us to create users and store them in the database
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
                        PhoneNumber = "00447869145567",
                        IsActive = true,
                        IsSuspended = false
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
                        PhoneNumber = "90447979164499",
                        IsActive = true,
                        IsSuspended = false
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
                        PhoneNumber = "00447779163399",
                        IsActive = true,
                        IsSuspended = false
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
                    var cat1 = new Category() { Name = "Announcements" };
                    var cat2 = new Category() { Name = "Blog Posts" };
                    var cat3 = new Category() { Name = "Reviews" };

                    //add each category to the Categories table
                    context.Categories.Add(cat1);
                    context.Categories.Add(cat2);
                    context.Categories.Add(cat3);

                    // save the changes to the database
                    context.SaveChanges();


                    //========================
                    // Seeding the Posts Table
                    //========================

                    //create a post
                    var post1 = new Post()
                    {
                        Title = "Edinburgh Theatre New Addition!",
                        Body = "The Theatre Royal Glasgow is home to Scotland’s resident companies and is a unique City Centre venue for conferences, and flexibility for many occasions. On March 2005 Theatre Group took over the management of the Theatre Royal Glasgow.",
                        User = member1,
                        Category = cat1
                    };

                    //add the post to the posts table
                    context.Posts.Add(post1);


                    var post2 = new Post()
                    {
                        Title = "Access to upper levels",
                        Body = "Following the redevelopment, new lifts have opened up access, meaning for the first time in the theatre's 147 year history all audience members can reach all levels.",
                        User = member1,
                        Category = cat1
                    };
                    context.Posts.Add(post2);


                    var post3 = new Post()
                    {
                        Title = "Theatre Royal Glasgow Cafe",
                        Body = "Customers can now enjoy the theatre during the day, in a new café - Vanilla Black at the Theatre - serving coffee, cakes and other tasty treats. Complimentary wi-fi is available for cafe-users.",
                        User = member1,
                        Category = cat1
                    };
                    context.Posts.Add(post3);


                    var post4 = new Post()
                    {
                        Title = "‘Aaraattu’ movie review: All-round star worship... and then some",
                        Body = "For the script is replete with those, with a few landing well and a majority falling flat. In some sequences, like when Neyyatinkara Gopan (Mohanlal) visits an old ‘tharavadu’, the references come so thick and fast, that it is hard to keep up.",
                        User = member1,
                        Category = cat3
                    };
                    context.Posts.Add(post4);


                    var post5 = new Post()
                    {
                        Title = "Opening Stage Revealed",
                        Body = "The ones made by Uwe Boll, who deserves his own category (Alone in the Dark, House of the Dead). We’re using a 20-review minimum cutoff for inclusion from theatrical releases only, because it’s not just enough to make a questionable movie, critics need to witness the aftermath, too.",
                        User = member1,
                        Category = cat1
                    };
                    context.Posts.Add(post5);


                    var post6 = new Post()
                    {
                        Title = "48 ACTION MOVIES RANKED",
                        Body = "It was in 1993 that Hollywood realized the dream of putting a video game movie up on the big screen with Super Mario Bros., and setting the stage for a long legacy of questionable choices, troubled productions, and gamers’ pixel tears left in their wake. But like the kid who just has to pump in one more quarter to reach for that high score, the studios keep on trying (while the fans just keep on hoping), and we’re celebrating that sort of sheer tenacity with this guide to the best video game movies (and plenty of the worst) ranked by Tomatometer!",
                        User = member1,
                        Category = cat2
                    };
                    context.Posts.Add(post6);

                    // save the changes to the database
                    context.SaveChanges();

                    //=============
                    // Add Comments
                    //=============
                    var Comment1p1 = new Comment()
                    {
                        Body = "too many Doors",
                        User = member2,
                        Post = post1
                    };
                    context.Comments.Add(Comment1p1);

                    // Add Comment
                    var Comment1p2 = new Comment()
                    {
                        Body = "too many Doors really too many",
                        User = member1,
                        Post = post1
                    };
                    context.Comments.Add(Comment1p2);

                    // Add Comment
                    var Comment2p1 = new Comment()
                    {
                        Body = "testy test",
                        User = member2,
                        Post = post2
                    };
                    context.Comments.Add(Comment2p1);

                    // save the changes to the database
                    context.SaveChanges();
                }// end if
            }// end if
        }// end seed method
    }// end class
}// end namespace