using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Blog.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class HomeServices :BlogPostDb,IHomeServices
    {
        public HomeServices(ApplicationDbContext dbContext) 
            : base(dbContext)
        {}

        public async Task<ICollection<string>> GetCategories()
        {
          return await DbContext.Categories.Select(x => x.Type).ToArrayAsync();
        }

        public async Task<ICollection<Post>> FilterByCategory(string category)
        {
            if (category!="all")
            {
              return await DbContext.Posts.Where(x => x.Categories.Select(c => c.Category.Type).Contains(category)).ToListAsync();
            }
            return await DbContext.Posts.ToArrayAsync();
        }
    }
}
