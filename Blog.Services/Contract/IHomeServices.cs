using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Services.Contract
{
    public interface IHomeServices
    {
       Task<ICollection<string>> GetCategories();
        Task<ICollection<Post>> FilterByCategory(string category);
    }
}