using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Services.Contract
{
    public interface ICommentServices
    {
        void Create(int id, string comment,User user);
        Task<int?> FindById(int id);
    }
}