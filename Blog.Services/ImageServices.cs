using System.Collections.Generic;
using Blog.Data;
using Blog.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class ImageServices : BlogPostDb, IImageServices
    {
        public ImageServices(ApplicationDbContext dbContext)
            : base(dbContext)
        { }

        public FileStreamResult ImageByName(string imageName)
        {
            var image = DbContext.Images.FirstOrDefault(i => i.Name == imageName);
            if (image != null)
            {

                MemoryStream memoryStream = new MemoryStream(image.Img);
                FileStreamResult result = new FileStreamResult(memoryStream, "image/jpg");
                result.FileDownloadName = imageName;
                return result;
            }

            return null;
        }

        public async void Create(Image image, List<IFormFile> img)
        {
            foreach (var imgFile in img)
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

            DbContext.Images.Add(image);
            DbContext.SaveChanges();
        }

        public async Task<List<Image>> AllImages()
        {
            return await DbContext.Images.ToListAsync();
        }

        public async Task<Image> ImageById(int id)
        {
            return await DbContext.Images
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public  void ImageDelete(int id, IFormCollection collection)
        {
            var images =  DbContext.Images.Find(id);
            DbContext.Images.Remove(images);
             DbContext.SaveChangesAsync();
        }

       
    }
}
