using System.Collections.Generic;
using System.Linq;
using DevBlog.Entities;
using DevBlog.Models;
using DevBlog.Repositories;

namespace DevBlog.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly PostTagRepository _postTagRepository;
        private readonly TagService _tagService;

        public PostService(PostRepository postRepository, 
            TagService tagService,
            PostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _tagService = tagService;
            _postTagRepository = postTagRepository;
        }

        public void Save(PostModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content.Replace("\n","<br />")
            };

            _postRepository.Create(post);

            var tags = model.Tags.Split(',');
            AddTagsToPost(post.Id, tags);
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

            result.ForEach((post) => 
            {
                if(post.Content.Length > 400)
                {
                    post.Content = post.Content.Substring(0, 400) + "...";
                }
            });

            return result;
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
