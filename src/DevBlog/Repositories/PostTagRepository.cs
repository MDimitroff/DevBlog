using DevBlog.Entities;
using DevBlog.Repositories.Abstract;

namespace DevBlog.Repositories
{
    public class PostTagRepository : BaseRepository<PostTag>
    {
        public PostTagRepository(BlogContext context)
            : base(context)
        { }
    }
}
