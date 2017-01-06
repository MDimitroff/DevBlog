using DevBlog.Models;
using DevBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly PostService _postService;

        public BlogPostController(PostService postService)
        {
            _postService = postService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(PostModel model)
        {
            _postService.Save(model);

            return View(nameof(List));
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
