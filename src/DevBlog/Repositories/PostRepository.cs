using System.Collections.Generic;
using System.Linq;
using DevBlog.Entities;
using DevBlog.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(BlogContext context)
            : base(context)
        { }

        public List<Post> Get()
        {
            return Context.Posts
                .Include(post => post.Tags)
                    .ThenInclude(tag => tag.Tag)
                .ToList();
        } 
    }
}
