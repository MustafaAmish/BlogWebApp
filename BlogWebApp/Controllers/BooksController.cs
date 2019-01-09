using Blog.Models;
using Blog.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookServices _bookServices;

        public BooksController(IBookServices bookServices)
        {
            this._bookServices = bookServices;
        }

        public async Task<ActionResult> Gallery()
        {
            return View(await _bookServices.AllBooks());
        }

        // GET: Books image
        public ActionResult Index(string imageName)
        {
            return _bookServices.BookImage(imageName);
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int id)
        {

            return View(await _bookServices.BookById(id));
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
        public ActionResult Create(Book book, List<IFormFile> image)

        {
            _bookServices.CreatBook(book, image);
            return RedirectToAction("Gallery");
        }

        // GET: Books/Delete/5

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var post = await _bookServices.BookById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _bookServices.Delete(id, collection);
            return RedirectToAction(nameof(Gallery));
        }
    }
}