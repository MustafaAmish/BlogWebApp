using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Services.Contract;
using BlogWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeServices _homeServices;

        public HomeController(IHomeServices homeServices)
        {
            _homeServices = homeServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string data="all")
        {
            var cat = new PostAndCategoryDto()
            {
                Categories = await _homeServices.GetCategories(),
                Posts = await _homeServices.FilterByCategory(data)
            };
            ViewData["Genre"] = "all";
            if (data != "all" && data != null)
            {
                ViewData["Genre"] = data;
               return View(cat);
            }
            return View(cat);
        }

        public IActionResult Privacy()
        {
            return View();
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
