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
                Content = model.Content
            };

            _postRepository.Create(post);

            var tags = model.Tags.Split(',');
            AddTagsToPost(post.Id, tags);
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
