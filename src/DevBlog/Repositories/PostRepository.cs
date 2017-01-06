using DevBlog.Entities;
using DevBlog.Repositories.Abstract;

namespace DevBlog.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(BlogContext context)
            : base(context)
        { }


    }
}
