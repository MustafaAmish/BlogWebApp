using Blog.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.Controllers
{
    public class UserController : Controller
    {

        private readonly IUsersService usersService;

        public UserController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Register() => this.View();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
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
        [Required()]
        [MinLength(3, ErrorMessage = "Must be 3 or more symbols!")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must be 3 or more symbols!")]
        public string LastName { get; set; }

        [Required()]
        [MinLength(10, ErrorMessage = "Must be 10 or more symbols!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Must be 6 or more symbols!")]
        public string Password { get; set; }

        [Required()]
        [MinLength(6, ErrorMessage = "Must be 6 or more symbols!")]
        public string ConfirmPassword { get; set; }
    }
}