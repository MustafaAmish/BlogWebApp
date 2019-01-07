using Blog.Data;
using Blog.Models;
using Blog.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class PostsServices : IPostSevices

    {
        private readonly ApplicationDbContext _context;

        public PostsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreateOrEdit(int id, Post post)
        {
            if (_context.PostCategoryses.Any(x => x.PostId == post.Id))
            {
                var catt = _context.PostCategoryses.Where(x => x.PostId == post.Id).ToArray();
                _context.PostCategoryses.RemoveRange(catt);

            }

            var curentPost =await CreateOrEdit(post);
            return curentPost;

        }

        public async Task<Post> PostById(int? id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return null;
            }
            var postModel =new Post()
            {
                Id = post.Id,
                Genre = post.Genre,
                Categories = post.Categories,
                Title = post.Title,
                Description = post.Description,
                CreatedOn = post.CreatedOn
            };
            return postModel;
        }

        public async Task<Post> CreateOrEdit(Post post)
        {
           

            var categoryAsStrings = post.Genre.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var categorys = new List<Category>();
            foreach (var type in categoryAsStrings)
            {
                var genre = await _context.Categories.FirstOrDefaultAsync(x =>
                    String.Equals(x.Type, type, StringComparison.CurrentCultureIgnoreCase));
                if (genre == null)
                {
                    var category = new Category() { Type = type };
                    categorys.Add(category);
                    _context.Categories.Add(category);
                }
                else
                {
                    categorys.Add(await _context.Categories.FirstAsync(x =>
                        String.Equals(x.Type, type, StringComparison.CurrentCultureIgnoreCase)));
                }

            }

            var cat = new List<PostCategorys>();
            foreach (var category1 in categorys)
            {
                cat.Add(new PostCategorys()
                {
                    Post = post,
                    Category = category1
                });
            }

            post.Categories = cat;
            return post;
        }
    }
}
