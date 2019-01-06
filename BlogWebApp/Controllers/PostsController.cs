using System;
using System.Collections.Generic;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;

namespace BlogWebApp.Controllers
{
    
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Genre")] Post post)
        {
            if (ModelState.IsValid)
            {
                var categoryAsString = post.Genre.Split(new[] {',', ' '}).ToArray();
                var categorys = new List<Category>();
                foreach (var type in categoryAsString)
                {
                    if (await _context.Categories.AnyAsync(x=>type == null || !String.Equals(x.Type, type, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        var category = new Category() {Type = type};
                        categorys.Add(category);
                        _context.Categories.Add(category);
                    }
                    else
                    {
                        categorys.Add(await _context.Categories.FirstAsync(x=>String.Equals(x.Type, type, StringComparison.CurrentCultureIgnoreCase)));
                    }
                }
               var cat=new List<PostCategorys>();
                foreach (var category1 in categorys)
                {
                    cat.Add(new PostCategorys()
                    {
                        Post = post,
                        Category = category1
                    });
                }

                post.Categoryses = cat;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,GenreString")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_context.PostCategoryses.Any(x => x.PostId == post.Id))
                    {
                        var catt = _context.PostCategoryses.Where(x => x.PostId == post.Id);
                        _context.PostCategoryses.RemoveRange(catt);
                    }
                   
                    var categoryAsStrings = post.Genre.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    var categorys = new List<Category>();
                    foreach (var type in categoryAsStrings)
                    {
                        if (!await _context.Categories.AnyAsync(x => type == null || x.Type.ToLower() != type.ToLower()))
                        {
                            var category = new Category() { Type = type };
                            categorys.Add(category);
                            _context.Categories.Add(category);
                        }
                        else
                        {
                            categorys.Add(await _context.Categories.FirstAsync(x => String.Equals(x.Type, type, StringComparison.CurrentCultureIgnoreCase)));
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
                    post.Categoryses = cat;
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
