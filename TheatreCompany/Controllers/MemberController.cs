// Author: Martin McCurley | Date: 05/02/22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using TheatreCompany.Models;
using System.Net;

namespace TheatreCompany.Controllers
{
    [Authorize(Roles = "Admin, Member")] // Allows both to access this Controller
    public class MemberController : Controller
    {
        // Instance of the database
        private TheatreCompanyDbContext db = new TheatreCompanyDbContext();

        // GET: Posts
        // The index action is called when registered users click the link "My Posts"
        // This method is returning a list of posts that were created by the logged in user (using userId)
        [Authorize(Roles = "Member")] // Only registered user as member role can access this method
        public ActionResult Index()
        {
            // Select all the posts from the posts table including foreign keys category and User
            var posts = db.Posts
                .Include(p => p.Category)
                .Include(p => p.User);

            // Get the id of the logged in user using IDENTITY. User Id is a string
            var userId = User.Identity.GetUserId();

            // From list of posts from posts table sekect only those which == the logged in user and return the posts
            posts = posts.Where(p => p.UserId == userId);

            // Send ther list of posts to the Index view in the Members subfolder
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id) // ? creates a nullable variable
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find a post in the post table by id
            Post post = db.Posts.Find(id);

            // If post doesnt exist then return error
            if (post == null)
            {
                return HttpNotFound();
            }

            // Otherwise send the post to the Details view and display the values stroed in the properties
            return View(post);
        }

        // GET: Posts/Edit/5
        // This method returns the edit form to the browser w/ an instance of post for user to make changes
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find post by an Id in the posts table
            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            // Get list of categories from Categories table and send the list to view using ViewBag
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            // Also send the post to ther Edit View where the user can change details of the post
            return View(post);
        }

        // POST: Posts/Edit/5
        // This method gets the edited/modified post and updates the changes in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Description,Location,Price,CategoryId")] Post post)
        {
            // As long as passed post isnt null then it is updated in the databse
            if (ModelState.IsValid)
            {
                // Gets the id of the user that is logged in the system and assigned it as a foreign key in the post
                post.UserId = User.Identity.GetUserId();
                // Updates the database
                db.Entry(post).State = EntityState.Modified;
                // Saves changes to the database
                db.SaveChanges();
                // Redirects user to index action in membercontroller which displays list of posts
                return RedirectToAction("Index");
            }
            // Otherwise if the post parameter is null then send the list of categories back to the edit form
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            // Return the post to the edit form
            return View(post);
        }

        // GET: Posts/Delete/5
        // This method will delete a post by id
        public ActionResult Delete(int? id)
        {
            // If id is null return error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // First find a post in the posts tabke by id
            Post post = db.Posts.Find(id);

            // Then find the post category by searching the categories table by categoryid which is the posts foreign key
            var category = db.Categories.Find(post.CategoryId);

            // Assign the category to the category navigational property so that we can display the category name
            post.Category = category;

            // If the post is a null object then return a not found error message
            if (post == null)
            {
                return HttpNotFound();
            };

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")] // This is IMPORTANT
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Find post by id in posts table
            Post post = db.Posts.Find(id);

            // Remove post from the posts table
            db.Posts.Remove(post);

            // Save changes in the database
            db.SaveChanges();

            // Redirect to the index action in the membercontroller
            return RedirectToAction("Index");
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            // Send the list of categories to the viewe using ViewBag so user can select from dropdown box
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");

            // Return the Create view to the browser
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,Description,Location,Price,CategoryId")] Post post)
        {
            // If parameter is not null
            if(ModelState.IsValid)
            {

                // Assign registered userid as foreign key as this is who created the post
                post.UserId = User.Identity.GetUserId();

                // Add the post to the posts tables
                db.Posts.Add(post);

                // Save changes in the database
                db.SaveChanges();

                // Return to index action in membercontroller
                return RedirectToAction("Index");

            }

            // If the parameter post is null then send the list categories back to the create view and try to create post again
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            // Send the post back to the create view
            return View(post);
        }
        
        /*=================================================
         * Do I need the rest of this?? Keep until complete
         * ================================================
         * 
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        // GET: Member/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Member/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}
