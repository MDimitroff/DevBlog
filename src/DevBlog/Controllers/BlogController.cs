﻿using DevBlog.Models;
using DevBlog.Services;
using DevBlog.Services.Elasticsearch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly PostService _postService;
        private readonly ElasticService _elasticService;

        public BlogController(PostService postService,
            ElasticService elasticService)
        {
            _postService = postService;
            _elasticService = elasticService;
        }

        public IActionResult Search()
        {
            var model = new List<PostModel>();

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(string terms)
        {
            var model = _elasticService.Search(terms);
            ViewBag.Terms = terms;

            return View(model);
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
            if (!TryValidateModel(model))
            {
                return View(model);
            }

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

        public IActionResult Delete(int postId)
        {
            _postService.Delete(postId);

            return Redirect("~/Blog/List");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
