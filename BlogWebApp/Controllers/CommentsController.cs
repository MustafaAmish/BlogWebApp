using Blog.Models;
using Blog.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogWebApp.Controllers
{
    public class CommentsController : Controller
    {


        private readonly UserManager<User> userManager;
        private readonly ICommentServices _commentServices;

        public CommentsController(UserManager<User> userManagerr, ICommentServices commentServices)
        {
            userManager = userManagerr;
            this._commentServices = commentServices;
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

            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);

            _commentServices.Create(id, comment, user);

            return Redirect("/Posts/Details/" + id);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentServices.FindById(id);

            if (comment != null)
            {
                return Redirect("/Posts/Details/" + comment);
            }

            return Redirect("/");
        }
    }
}