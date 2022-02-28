// Author: Martin McCurley | Date: 01/02/22
using TheatreCompany.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheatreCompany.Models;

namespace TheatreCompany.Controllers
{

    [Authorize(Roles = "Admin")] // This allows only admins to access the admin controller
    public class AdminController : AccountController
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

        // View All Users Action
        [Authorize(Roles = "Admin")]
        public ActionResult ViewUsers()
        {
            // Get all registered users including roles and order them by last name
            List<User> users = db.Users.Include(u => u.Roles).OrderBy(u => u.LastName).ToList();

            // Send the list to the view and display
            return View(users);
        }

        #endregion
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> ChangeRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Admin");
            }

            User user = await UserManager.FindByIdAsync(id);
            string oldRole = (await UserManager.GetRolesAsync(id)).Single();

            var items = db.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name,
                Selected = r.Name == oldRole
            }).ToList();

            return View(new ChangeRoleViewModel
            {
                UserName = user.UserName,
                Roles = items,
                OldRole = oldRole
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangeRole")]
        public async Task<ActionResult> ChangeRoleConfirmed(string id, [Bind(Include = "Role")] ChangeRoleViewModel model)
        {
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByIdAsync(id);
                string oldRole = (await UserManager.GetRolesAsync(id)).Single();

                if (oldRole == model.Role)
                {
                    return RedirectToAction("Index", "Admin");
                }

                await UserManager.RemoveFromRoleAsync(id, oldRole);
                await UserManager.AddToRoleAsync(id, model.Role);

                if (model.Role == "Suspended")
                {
                    user.IsSuspended = true;

                    await UserManager.UpdateAsync(user);
                }
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
        }



        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Admin");
            }

            User user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            // Delete comments where the commentId is from that user
            var comments = db.Comments.Where(o => o.UserId == id).ToList();
            db.Comments.RemoveRange(comments);

            // Delete comments where the postId is from that user
            var posts = db.Posts.Where(o => o.UserId == id).ToList();
            db.Posts.RemoveRange(posts);

            // Save db change
            db.SaveChanges();

            // Finally delete user after posts and comments have been removed to avoid FK constraint breach
            await UserManager.DeleteAsync(user);

            return RedirectToAction("Index", "Admin");
        }



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

        // GET: Posts/Details/5
        public ActionResult PostDetails(int? id)
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

        // GET: Posts/Edit/5
        public ActionResult EditPost(int? id)
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
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "PostId,Title,Body,UserId,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", post.UserId);
            return View(post);
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
