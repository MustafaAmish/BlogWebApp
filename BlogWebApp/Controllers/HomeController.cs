using System;
using BlogWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string data)
        {
            ViewData["Genre"] = "all";
            if (data != "all" && data != null)
            {
                ViewData["Genre"] = data;
                return View(await _context.Posts.Where(x => x.Genre.ToString() == data).ToListAsync());
            }
            return View(await _context.Posts.ToListAsync());
        }
        //[HttpPost]
        //public async Task<IActionResult> Index(string id)
        //{
        //    if (id != "all")
        //    {
        //        return View(await _context.Posts.Where(x => x.Genre.ToString() == id).ToListAsync());
        //    }
        //    return View(await _context.Posts.ToListAsync());
        //}
        [Authorize(Roles = "Admin")]
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
