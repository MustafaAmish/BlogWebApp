using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Blog.Services.Contract;
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
        private readonly IImageServicesaaaaaaaaaa imageServices;
        public ImageController(ApplicationDbContext context, IImageServicesaaaaaaaaaa imgServices)
        {
            _context = context;
            _image = context.Images.ToArray();
            this.imageServices = imgServices;
        }

        public async Task<IActionResult> Gallery()
        {
            
            return View(await imageServices.AllImages());
        }

        public async Task<IActionResult> Details(int id )
        {
            var img =await imageServices.ImageById(id);
            return View( img);
        }
        [Authorize(Roles = "Admin")]
        public  IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Image image, List<IFormFile> Img)
        {
            imageServices.Create(image,Img);
            return RedirectToAction("Gallery");
        }

        public ActionResult Index(string imageName)
        {
            return imageServices.ImageByName(imageName);
        }

      
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var images = await imageServices.ImageById(id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // POST: Books/Delete/5
        [HttpPost]
     
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            imageServices.ImageDelete(id,collection);
            return RedirectToAction(nameof(Gallery));
        }
    }
}