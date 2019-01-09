using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blog.Services.Contract
{
    public interface IBookServices
    {
        Task<ICollection<Book>> AllBooks();
        FileStreamResult BookImage(string imageName);
        Task<Book> BookById(int id);
        void CreatBook(Book book, List<IFormFile> image);
        void Delete(int id, IFormCollection collection);
    }
}