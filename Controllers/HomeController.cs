using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TheatreCompany.Models;
using TheatreCompany.Data;

namespace TheatreCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            var posts = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.User)
                .Where(p => p.Category.Name.Contains(searchString))
                .ToListAsync();

            return View(posts);
        }
    }
}