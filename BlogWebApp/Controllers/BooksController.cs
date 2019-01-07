using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<ActionResult> Gallery()
        {
           return View(await _context.Books.ToArrayAsync());
        }

        // GET: Books image
        public ActionResult Index(string imageName)
        {
            var image = _context.Books.FirstOrDefault(i => i.Title == imageName);
            if (image != null)
            {

                MemoryStream memoryStream = new MemoryStream(image.Image);
                FileStreamResult result = new FileStreamResult(memoryStream, "image/jpg");
                result.FileDownloadName = imageName;
                return result;
            }

            return null;
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var book = _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return View(await book);
        }

       
        [Authorize(Roles = "Admin")]
       public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Book book, List<IFormFile> Image)
            
        {
            foreach (var imgFile in Image)
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

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Gallery");
        }

        // GET: Books/Delete/5
       
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Books/Delete/5
        [HttpPost]
    
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Gallery));
        }
    }
}