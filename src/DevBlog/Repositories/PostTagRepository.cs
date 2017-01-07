using DevBlog.Entities;
using DevBlog.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace DevBlog.Repositories
{
    public class PostTagRepository : BaseRepository<PostTag>
    {
        public PostTagRepository(BlogContext context)
            : base(context)
        { }

        public List<PostTag> GetBy(int postId)
        {
            return Context.PostTags
                .Where(tag => tag.PostId == postId)
                .ToList();
        }

        public List<PostTag> GetBy(int postId, string[] tagName)
        {
            return Context.PostTags
                .Where(p => p.PostId == postId && tagName.Contains(p.Tag.Name))
                .ToList();
        }
    }
}
