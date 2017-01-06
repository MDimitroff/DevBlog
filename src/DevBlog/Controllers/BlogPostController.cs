using DevBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers
{
    public class BlogPostController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(PostModel model)
        {


            return View(nameof(List));
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
