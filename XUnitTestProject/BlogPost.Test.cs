using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Blog.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        
        
        [Fact]
        public void TestPostServicesMethodAllPost()
        {
            var options=new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "BlogWebApp").Options;
            var db=new ApplicationDbContext(options);
            var posts = new[]
            {
                new Post {Title = "First Post", Description = "IT", Genre = "it"},
                new Post { Title = "First Post", Description = "IT", Genre = "it"}
            };
            db.Posts.AddRange(posts);
            db.SaveChangesAsync();

            var count=new PostsServices(db);
            var result = count.AllPosts();
            Assert.NotEqual(1, result.Result.Count);
            Assert.Equal(2, result.Result.Count);
            
        }
        //Testing Post create 
        [Fact]
        public async void TestPostServicesMethodCreateOrEdit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "BlogWebApp").Options;
            var db = new ApplicationDbContext(options);

            var post1 = new Post {Title = "First Post", Description = "IT", Genre = "it"};
            var post2 = new Post { Description = "IT", Genre = "it"};
            var post3 = new Post { Title = "First Post",  Genre = "it" };
            var post4 = new Post { Title = "First Post", Description = "IT" };


            var context = new PostsServices(db);
            //Add post with valid data
            var result = await context.CreateOrEdit(post1);
            Assert.Equal(post1, result);
            Assert.Equal(5,post1.Id);

            Task<InvalidDataException> exception= Assert.ThrowsAsync<InvalidDataException>(async ()=>await context.CreateOrEdit(post2));
            Assert.Equal("no empty fields allowed ",exception.Result.Message);


            Task<InvalidDataException> exception2 = Assert.ThrowsAsync<InvalidDataException>(async () => await context.CreateOrEdit(post3));
            Assert.Equal("no empty fields allowed ", exception2.Result.Message);

            Task<InvalidDataException> exception3 = Assert.ThrowsAsync<InvalidDataException>(async () => await context.CreateOrEdit(post4));
            Assert.Equal("no empty fields allowed ", exception3.Result.Message);

        }

        [Fact]
        public async void TestPostServicesMethodPostById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "BlogWebApp").Options;
            var db = new ApplicationDbContext(options);
            var context = new PostsServices(db);
            var post1 = new Post { Title = "First Post", Description = "Kuku", Genre = "it" };
            
          
            var post1Edit = new Post { Title = "Second Post", Description = "Test", Genre = "it" };
             await context.CreateOrEdit(post1);
             await context.CreateOrEdit(post1Edit);


            await Task.Delay(10000);
            //Add post with valid data
            var firstPostById = await context.PostById(1);
            Assert.Equal("First Post", firstPostById.Title);
            Assert.Equal(1, firstPostById.Id);
            var secondPostById = await context.PostById(2);
            Assert.Equal("First Post", secondPostById.Title);
            Assert.Equal(2, secondPostById.Id);

            //Task<InvalidDataException> exception = Assert.ThrowsAsync<InvalidDataException>(async () => await context.CreateOrEdit(post2));
            //Assert.Equal("no empty fields allowed ", exception.Result.Message);


            //Task<InvalidDataException> exception2 = Assert.ThrowsAsync<InvalidDataException>(async () => await context.CreateOrEdit(post3));
            //Assert.Equal("no empty fields allowed ", exception2.Result.Message);

            //Task<InvalidDataException> exception3 = Assert.ThrowsAsync<InvalidDataException>(async () => await context.CreateOrEdit(post4));
            //Assert.Equal("no empty fields allowed ", exception3.Result.Message);

        }

        private void Seed(ApplicationDbContext db)
        {
            var posts = new[]
            {
                new Post {Title = "First Post", Description = "IT", Genre = "it"},
                new Post { Title = "First Post", Description = "IT", Genre = "it"}
            };
            db.Posts.AddRange(posts);
            db.SaveChangesAsync();
        }
    }
}
