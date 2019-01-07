using AutoMapper;
using Blog.Models;
using BlogWebApp.Models;

namespace BlogWebApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<PostModel, Post>();
            CreateMap<Post, PostModel>();
        }
    }
}
