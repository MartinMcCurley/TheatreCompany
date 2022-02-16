using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCompany.Models;

namespace TheatreCompany.Controllers
{

    [Authorize(Roles = "Admin")] // This allows only admins to access the admin controller
    public class AdminController : Controller
    {
        #region Admin roles
        // Create an instance of the database so we can access the tables
        private TheatreCompanyDbContext db = new TheatreCompanyDbContext();

        // GET: Admin
        [Authorize(Roles = "Admin")] // Only admins can call the admin index action
        public ActionResult Index()
        {
            return View();
        }

        // View All Posts Action
        [Authorize(Roles = "Admin")]
        public ActionResult ViewUsers()
        {
            // Get all registered users including roles and order them by last name
            List<User> users = db.Users.Include(u => u.Roles).OrderBy(u => u.LastName).ToList();

            // Send the list to the view and display
            return View(users);
        }

        #endregion

        #region Category Actions
        // GET: Categories
        public ActionResult ViewAllCategories()
        {
            // Return the ViewAllCategories view that displays a list of categories
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find category by id in cateegories table
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // Send the category to the details view
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("ViewAllCategories");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAllCategories");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("ViewAllCategories");
        }
        #endregion

        #region Post Actions
        // View All Posts Action
        [Authorize(Roles = "Admin")]
        public ActionResult ViewAllPosts()
        {
            // Get all posts from the database including their category and user who created the post
            List<Post> posts = db.Posts.Include(p => p.Category).Include(p => p.User).ToList();

            // Send the list t the vierw named viewallposts
            return View(posts);
        }


        // GET: Posts/Delete/5
        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("ViewAllPosts");
        }
        #endregion

        #region Comment Actions
        // GET: Comments
        public ActionResult ViewAllComments()
        {
            // Return the ViewAllComments view that displays a list of Comments
            return View(db.Comments.ToList());
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Name")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("ViewAllComments");
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
