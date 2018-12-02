using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    public class UserController : Controller
    {

        private readonly IUsersService usersService;

        public UserController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index() => this.View();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = this.usersService.Register( model.Email, model.Password, model.ConfirmPassword, model.FirstName, model.LastName).GetAwaiter().GetResult();

            if (!result)
            {
                return this.View(model);
            }

            return this.RedirectToAction(nameof(Login));
        }

        public IActionResult Login() => this.View();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = this.usersService.Login(model.Username, model.Password, model.RememberMe);

            if (!result)
            {
                return this.View(model);
            }

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.usersService.Logout();
            return this.Redirect("/");
        }
    }

    public class LoginUserViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterUserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}