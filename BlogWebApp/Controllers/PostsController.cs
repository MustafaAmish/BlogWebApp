using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Data;
using Blog.Models;
using Blog.Services.Contract;
using BlogWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Controllers
{

    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPostSevices _postSevices;

        public PostsController(ApplicationDbContext context, IMapper mapper, IPostSevices postSevices)
        {
            _context = context;
            _mapper = mapper;
            _postSevices = postSevices;
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

            var post = await _postSevices.PostById(id);
            if (post == null)
            {
                return NotFound();
            }
            var newPost = _mapper.Map<PostModel>(post);
            return View(newPost);
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Genre")] PostModel postModel)
        {
            var post = _mapper.Map<Post>(postModel);
            if (ModelState.IsValid)
            {

                post = await _postSevices.CreateOrEdit(post);
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var newPost = _mapper.Map<PostModel>(post);
            return View(newPost);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postSevices.PostById(id);
            if (post == null)
            {
                return NotFound();
            }

            var postModel = _mapper.Map<PostModel>(post);
            return View(postModel);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Genre")] PostModel postModel)
        {
            var post = _mapper.Map<Post>(postModel);
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post = await _postSevices.CreateOrEdit(id, post);
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
                        throw new DataException("aaaaaaa");
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var newPost = _mapper.Map<PostModel>(post);
            return View(newPost);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postSevices.PostById(id);
            if (post == null)
            {
                return NotFound();
            }

            var curentPost = _mapper.Map<PostModel>(post);
            return View(curentPost);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postSevices.Delete(id);
            if (post)
            {
                return RedirectToAction(nameof(Index));
            }
            throw new InvalidDataException("Poster with given ID not found");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
