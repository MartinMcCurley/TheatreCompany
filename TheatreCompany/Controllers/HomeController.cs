using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using TheatreCompany.Models;

namespace TheatreCompany.Controllers
{
    public class HomeController : Controller
    {
        // Create an instance of the database context
        private TheatreCompanyDbContext context = new TheatreCompanyDbContext();


        public ActionResult Index()
        {
            // Get all posts, include category for each post, include the user who created the post
            // and order the posts from most current to old posts
            var posts = context.Posts.Include(p => p.Category).Include(p => p.User).OrderByDescending(p => p.DatePosted);

            // Send the list of categories over the index page so we can display them
            ViewBag.Categories = context.Categories.ToList();

            // Send the posts collection to the view named Index
            return View(posts.ToList());

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(int id)
        {
            // Search the Posts table in the database, find the post by id and return the post
            Post post = context.Posts.Find(id);

            // Using the foreign key UserId from the post instance find the user who created the post
            var user = context.Users.Find(post.UserId);

            // Using the foreign key CategoryId from the post find the category that the post belongs to
            var category = context.Categories.Find(post.CategoryId);

            // Assign the user to the User navigational property in Post
            post.User = user;

            // Assign the category to the Category navigational property in Post
            post.Category = category;

            // Send the post model to the Details View
            return View(post);
        }


        // This will process the search string from the index page, it must have the same name as the textbox on the view
        [HttpPost]
        public ViewResult Index(string SearchString)
        {
            var posts = context.Posts.Include(p => p.Category).Include(p => p.User).Where(p => p.Category.Name.Equals(SearchString.Trim())).OrderByDescending(p => p.DatePosted);
          
            return View(posts.ToList());
        }
    }
}