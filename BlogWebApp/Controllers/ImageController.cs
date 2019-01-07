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
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Image[] _image;
        public ImageController(ApplicationDbContext context)
        {
            _context = context;
            _image = context.Images.ToArray();
        }

        public async Task<IActionResult> Gallery()
        {
            
            return View(await _context.Images.ToArrayAsync());
        }

        public async Task<IActionResult> Details(int id )
        {
            var img = _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            return View(await img);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Image image, List<IFormFile> Img)
        {
            foreach (var imgFile in Img)
            {
                if (imgFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await imgFile.CopyToAsync(stream);
                        image.Img = stream.ToArray();
                    }
                }
            }
            
            _context.Images.Add(image);
            _context.SaveChanges();
            return RedirectToAction("Gallery");
        }

        public ActionResult Index(string imageName)
        {
            var image = _context.Images.FirstOrDefault(i => i.Name == imageName);
            if (image != null)
            {

                MemoryStream memoryStream = new MemoryStream(image.Img);
                FileStreamResult result = new FileStreamResult(memoryStream, "image/jpg");
                result.FileDownloadName = imageName;
                return result;
            }

            return null;
        }

      
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var images = await _context.Images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (_image == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // POST: Books/Delete/5
        [HttpPost]
     
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var images = await _context.Images.FindAsync(id);
            _context.Images.Remove(images);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Gallery));
        }
    }
}