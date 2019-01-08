using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public  class BlogPostDb 
    {
        public BlogPostDb(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ApplicationDbContext DbContext { get; set; }
    }
}
