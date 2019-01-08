using Blog.Data;
using Blog.Models;
using Blog.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class CommentServices : BlogPostDb, ICommentServices
    {
        public CommentServices(ApplicationDbContext dbContext)
            : base(dbContext)
        { }

        public void Create(int id, string comment, User user)
        {
            var mm = new Comment()
            {
                Content = comment,
                User = user,
                Post = this.DbContext.Posts.First(x => x.Id == id)
            };
            this.DbContext.Comments.Add(mm);
            this.DbContext.SaveChanges();
        }

        public async Task<int?> FindById(int id)
        {
            var comment = await DbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment != null)
            {
                this.DbContext.Comments.Remove(comment);
                this.DbContext.SaveChanges();
                return comment.PostId;
            }

            return null;
        }
    }
}
