using Blog.Data;
using Blog.Models;
using Blog.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;

namespace Blog.Services
{
    public class PostsServices : BlogPostDb, IPostSevices

    {
        public PostsServices(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Post> CreateOrEdit(int id, Post post)
        {
            if (DbContext.PostCategoryses.Any(x => x.PostId == post.Id))
            {

                var catt = DbContext.PostCategoryses.Where(x => x.PostId == post.Id).ToArray();
                DbContext.PostCategoryses.RemoveRange(catt);
                 DbContext.SaveChanges();
            }

            var curentPost = await CreateOrEdit(post);
           
            return curentPost;
        }

        public async Task<Post> PostById(int? id)
        {
            var post = await DbContext.Posts.FindAsync(id);

            if (post == null)
            {
                return null;
            }

            var postModel = new Post()
            {
                Id = post.Id,
                Genre = post.Genre,
                Categories = post.Categories,
                Title = post.Title,
                Description = post.Description,
                CreatedOn = post.CreatedOn,
                Comments = post.Comments
            };

            return postModel;
        }

        public async Task<bool> Delete(int id)
        {
            var post = await DbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post != null)
            {
                DbContext.Posts.Remove(post);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ICollection<Post>> AllPosts()
        {
            return await DbContext.Posts.ToListAsync();
        }
        
        public async Task<Post> CreateOrEdit(Post post)
        {
            var isNewPost = post.Id == 0;
            if (post.Genre.IsNullOrEmpty()
                || post.Title.IsNullOrEmpty() || post.Description.IsNullOrEmpty())
            {
                throw new InvalidDataException("no empty fields allowed ");
            }

            var categoryAsStrings = post.Genre.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var categorys = new List<Category>();
            foreach (var type in categoryAsStrings)
            {
                var genre = await DbContext.Categories.FirstOrDefaultAsync(x =>
                    String.Equals(x.Type, type, StringComparison.CurrentCultureIgnoreCase));
                if (genre == null)
                {
                    var category = new Category() { Type = type };
                    categorys.Add(category);
                    DbContext.Categories.Add(category);
                }
                else
                {
                    categorys.Add(await DbContext.Categories.FirstAsync(x =>
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
            if (isNewPost)
            {
               await DbContext.Posts.AddAsync(post);
            }
            else
            {
                DbContext.Posts.Update(post);
            }
            await DbContext.SaveChangesAsync();
            return post;
        }
    }
}
