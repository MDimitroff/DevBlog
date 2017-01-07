using DevBlog.Models;
using DevBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly PostService _postService;

        public BlogController(PostService postService)
        {
            _postService = postService;
        }

        public IActionResult Post(int? id)
        {
            PostModel model = null;
            if (id.HasValue && id.Value > 0)
            {
                model = _postService.Get(id.Value);
            }
            else
            {
                model = new PostModel();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Post(PostModel model)
        {
            _postService.SaveOrUpdate(model);

            return Redirect(nameof(List));
        }

        public IActionResult List()
        {
            var posts = _postService.Get();

            return View(posts);
        }

        public IActionResult View(int id)
        {
            var post = _postService.Get(id);

            return View(post);
        }

        public IActionResult Delete(int id)
        {
            _postService.Delete(id);

            return Redirect("~/Blog/List");
        }
    }
}
