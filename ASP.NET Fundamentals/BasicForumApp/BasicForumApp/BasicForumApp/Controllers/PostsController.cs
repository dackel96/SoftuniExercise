using BasicForumApp.Data;
using BasicForumApp.Data.Entities;
using BasicForumApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicForumApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly BasicForumAppDbContext context;

        public PostsController(BasicForumAppDbContext _context)
        {
            this.context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var posts = this.context
                .Posts
                .Select(x => new PostViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content
                })
                .ToList();

            return View(posts);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PostFormModel model)
        {
            var post = new Post()
            {
                Title = model.Title,
                Content = model.Content
            };

            this.context.Posts.Add(post);
            this.context.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var post = this.context.Posts.Find(id);

            var postModel = new PostFormModel();

            if (post != null)
            {
                postModel = new PostFormModel
                {
                    Title = post.Title,
                    Content = post.Content
                };
            }

            return View(postModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, PostFormModel model)
        {
            var post = this.context.Posts.Find(id);

            if (post != null)
            {
                post.Title = model.Title;

                post.Content = model.Content;

                this.context.SaveChanges();
            }

            return RedirectToAction("All");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var post = this.context.Posts.Find(id);

            if (post != null)
            {
                this.context.Posts.Remove(post);

                this.context.SaveChanges();
            }

            return RedirectToAction("All");
        }
    }
}
