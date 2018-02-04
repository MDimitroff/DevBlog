using DevBlog.Entities;
using DevBlog.Models;
using DevBlog.Repositories;
using DevBlog.Services.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevBlog.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly PostTagRepository _postTagRepository;
        private readonly TagService _tagService;
        private readonly ElasticService _elasticService;

        public PostService(PostRepository postRepository, 
            TagService tagService,
            PostTagRepository postTagRepository,
            ElasticService elasticService)
        {
            _postRepository = postRepository;
            _tagService = tagService;
            _postTagRepository = postTagRepository;
            _elasticService = elasticService;
        }

        public void SaveOrUpdate(PostModel model)
        {
            if (model.Id > 0)
            {
                Edit(model);
            }
            else
            {
                Save(model);
            }

            _elasticService.Index(model);
        }

        public List<PostModel> Get()
        {
            var result = _postRepository.Get()
                .Select(p => new PostModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content.Replace("<br />", string.Empty),
                    TagNames = (p.Tags.Any()) ? p.Tags.Select(t => t.Tag.Name).ToList() : null
                })
                .OrderByDescending(p => p.Id)
                .ToList();

            return result;
        }

        public PostModel Get(int id)
        {
            PostModel result = null;
            var post = _postRepository.Get()
                .FirstOrDefault(p => p.Id == id);

            if (post != null)
            {
                result = new PostModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    TagNames = (post.Tags.Any()) ? post.Tags.Select(t => t.Tag.Name).ToList() : null
                };
            }

            return result;
        }

        public void Delete(int postId)
        {
            var tags = _postTagRepository.GetBy(postId);
            var post = _postRepository.Get()
                .FirstOrDefault(p => p.Id == postId);

            if (post == null) return;

            foreach (var tag in tags)
            {
                _postTagRepository.Delete(tag);
            }

            _elasticService.Delete(post.Id); 
            _postRepository.Delete(post);
        }

        private void Save(PostModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content.Replace(Environment.NewLine, "<br />")
            };

            _postRepository.Create(post);
            model.Id = post.Id;

            var tags = model.Tags.Split(',');
            AddTagsToPost(post.Id, tags);
        }

        private void Edit(PostModel model)
        {
            var post = _postRepository.Get()
                .FirstOrDefault(p => p.Id == model.Id);

            if (post == null) return;

            post.Title = model.Title;
            post.Content = model.Content.Replace(Environment.NewLine, "<br />");

            _postRepository.SaveChanges();

            var newTags = model.Tags.Split(',');
            var oldTags = _postTagRepository.GetBy(post.Id)
                .Select(t => t.Tag.Name);

            var tagsToDelete = oldTags.Where(t => !newTags.Contains(t)).ToArray();
            DeleteTagsFromPost(post.Id, tagsToDelete);

            var tagsToAdd = newTags.Where(t => !oldTags.Contains(t)).ToArray();
            AddTagsToPost(post.Id, tagsToAdd);
        }

        private void DeleteTagsFromPost(int postId, string[] tagsToDelete)
        {
            var entitesToDelete = _postTagRepository.GetBy(postId, tagsToDelete);

            foreach (var tag in entitesToDelete)
            {
                _postTagRepository.Delete(tag);
            }
        }

        private void AddTagsToPost(int postId, string[] tags)
        {
            var tagIds = _tagService.InsertTags(tags);

            foreach (var tagId in tagIds)
            {
                var tagEntity = new PostTag
                {
                    PostId = postId,
                    TagId = tagId
                };

                _postTagRepository.Create(tagEntity);
            }
        }
    }
}
