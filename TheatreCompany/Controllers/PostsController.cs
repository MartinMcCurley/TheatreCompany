// Author: Martin McCurley | Date: 10/02/22
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCompany.Models;

namespace TheatreCompany.Controllers
{
    public class PostsController : Controller
    {
        private TheatreCompanyDbContext db = new TheatreCompanyDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category).Include(p => p.User);
            return View(posts.ToList());
        }

        [Authorize(Roles = "Admin, Staff, Member")]
        // GET: Posts/Details/5
        public ActionResult Details(int? id)
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

        [Authorize(Roles = "Admin, Staff, Member")]
        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff, Member")]
        public ActionResult Create([Bind(Include = "PostId,Title,Body,UserId,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", post.UserId);
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin, Staff, Member")]
        public ActionResult Edit(int? id)
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
        [Authorize(Roles = "Admin, Staff, Member")]
        public ActionResult Edit([Bind(Include = "PostId,Title,Body,UserId,CategoryId")] Post post)
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
        [Authorize(Roles = "Admin, Staff, Member")]
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff, Member")]
        public ActionResult DeleteConfirmed(int id)
        {
            var comments = db.Comments.Where(o => o.PostId == id).ToList();
            db.Comments.RemoveRange(comments);

            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // This will process the search string from the index page, it must have the same name as the textbox on the view
        [HttpPost]
        public ViewResult Index(string SearchString)
        {
            var posts = db.Posts.Include(p => p.Category).Include(p => p.User).Where(p => p.Category.Name.Equals(SearchString.Trim()));

            return View(posts.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
