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

        public IActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(PostModel model)
        {
            _postService.Save(model);

            return Redirect(nameof(List));
        }

        public IActionResult List()
        {
            var posts = _postService.Get();

            return View(posts);
        }
    }
}
