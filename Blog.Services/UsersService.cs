using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Services.Contract;
using Microsoft.AspNetCore.Identity;

namespace Blog.Services
{
    public class UsersService : IUsersService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public UsersService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public bool Login(string username, string password, bool rememberMe)
        {
            if (username == null ||
                password == null)
            {
                return false;
            }

            var result = this.signInManager.PasswordSignInAsync(username, password, rememberMe, false).GetAwaiter().GetResult();

            return result.Succeeded;
        }


        public void Logout()
        {
            this.signInManager.SignOutAsync().Wait();
        }

        public async Task<bool> Register( string email, string password, string confirmPassword, string firstName, string lastName)
        {
            if (email == null ||
                password == null ||
                confirmPassword == null ||
                firstName == null ||
                lastName == null ||
                password != confirmPassword)
            {
                return false;
            }
            
            var user = new User
            {
                Email = email,
                FistName = firstName,
                LastName = lastName,
                UserName = email
            };

            var result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }
            }

            return result.Succeeded;
        }
    }
}