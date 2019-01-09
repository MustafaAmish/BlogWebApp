using System;
using Blog.Data;
using Blog.Models;
using Blog.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services
{
    public class BookServices : BlogPostDb, IBookServices
    {
        public BookServices(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<ICollection<Book>> AllBooks()
        {
            return await DbContext.Books.ToArrayAsync();
        }

        public FileStreamResult BookImage(string imageName)
        {
            var image = DbContext.Books.FirstOrDefault(i => i.Title == imageName);
            if (image != null)
            {

                MemoryStream memoryStream = new MemoryStream(image.Image);
                FileStreamResult result = new FileStreamResult(memoryStream, "image/jpg");
                result.FileDownloadName = imageName;
                return  result;
            }

            return null;
        }

        public async Task<Book> BookById(int id)
        {
            return await DbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async void CreatBook(Book book, List<IFormFile> image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            foreach (var imgFile in image)
            {
                if (imgFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await imgFile.CopyToAsync(stream);
                        book.Image = stream.ToArray();
                    }
                }
            }

            DbContext.Books.Add(book);
            DbContext.SaveChanges();
           
        }

        public void Delete(int id, IFormCollection collection)
        {
            var book =  DbContext.Books.Find(id);
            DbContext.Books.Remove(book);
             DbContext.SaveChangesAsync();
        }
    }
}
