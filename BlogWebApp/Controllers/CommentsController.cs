using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using BlogWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    public class CommentsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;

        public CommentsController(ApplicationDbContext context, UserManager<User> userManagerr)
        {
            _context = context;
            userManager = userManagerr;
        }

        public IActionResult Create()   
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id,CommetDto modelCommetDto)
        {
            var mm = new Comment()
            {
                Content = modelCommetDto.Content,
                User =await this.userManager.FindByNameAsync(this.User.Identity.Name),
                Post = this._context.Posts.First(x=>x.Id==id)
            };
            this._context.Comments.Add(mm);
            this._context.SaveChanges();
            return Redirect("/");
        }
    }
}