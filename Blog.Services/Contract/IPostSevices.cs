using Blog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Services.Contract
{
    public interface IPostSevices
    {
        Task<Post> CreateOrEdit(Post post);
        Task<Post> CreateOrEdit(int id, Post post);
        Task<Post> PostById(int? id);
        Task<bool> Delete(int id);
        Task<ICollection<Post>> AllPosts();


    }
}