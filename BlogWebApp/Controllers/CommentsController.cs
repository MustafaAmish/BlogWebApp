using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }
            var mm = new Comment()
            {
                Content = comment,
                User =await this.userManager.FindByNameAsync(this.User.Identity.Name),
                Post = this._context.Posts.First(x=>x.Id==id)
            };
            this._context.Comments.Add(mm);
            this._context.SaveChanges();
            return Redirect("/Posts/Details/"+id);
        }
       
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment =await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment!=null)
            {
               this._context.Comments.Remove(comment);
            }
           
            this._context.SaveChanges();
            if (comment != null)
            {
                return Redirect("/Posts/Details/" + comment.PostId);
            }

            return Redirect("/");
        }
    }
}