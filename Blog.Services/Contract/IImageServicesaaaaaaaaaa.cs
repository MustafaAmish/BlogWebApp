using Blog.Models;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace Blog.Services.Contract
{
    public interface IImageServicesaaaaaaaaaa
    {
        FileStreamResult ImageByName(string imageName);
        void Create(Image image, List<IFormFile> img);
        Task<List<Image>> AllImages();
        Task<Image> ImageById(int id);
        void ImageDelete(int id, IFormCollection collection);

    }
}