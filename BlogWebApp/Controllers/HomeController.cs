using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using BlogWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var cat = new PostAndCategoryDto()
            {
                Categories = await _context.Categories.Select(x => x.Type).ToArrayAsync(),
                Posts = await _context.Posts.ToArrayAsync()
            };
            ViewData["Genre"] = "all";
            if (data != "all" && data != null)
            {
                ViewData["Genre"] = data;
                cat.Posts = await _context.Posts.Where(x => x.Categories.Select(c => c.Category.Type).Contains(data)).ToListAsync();
                return View(cat);
            }
            return View(cat);
        }
      
        

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
