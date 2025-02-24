using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TheatreCompany.Models;
using TheatreCompany.Data;

namespace TheatreCompany.Controllers
{
    [Authorize(Roles = "Member")]
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .Where(p => p.UserId == user.Id)
                .ToListAsync();

            return View(posts);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (post.UserId != currentUser.Id)
            {
                return Forbid();
            }

            return View(post);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                post.UserId = user.Id;
                post.CreatedAt = DateTime.UtcNow;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (post.UserId != user.Id)
            {
                return Forbid();
            }

            ViewData["CategoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CategoryId,UserId,CreatedAt")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (post.UserId != user.Id)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (post.UserId != user.Id)
            {
                return Forbid();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
