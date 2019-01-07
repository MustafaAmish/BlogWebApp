using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.Contract
{
    public interface IPostSevices
    {
        Task<Post> CreateOrEdit(Post post);
        Task<Post> CreateOrEdit(int id,Post post);
        Task<Post> PostById(int? id);
    }
}