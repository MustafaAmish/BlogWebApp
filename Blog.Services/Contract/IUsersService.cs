using System.Threading.Tasks;

namespace Blog.Services.Contract
{
    public interface IUsersService
    {
        bool Login(string firstName, string password, bool rememberMe);

        Task<bool> Register( string email, string password, string confirmPassword, string firstName, string lastName );

        void Logout();
    }
}
