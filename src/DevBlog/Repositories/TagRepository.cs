using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevBlog.Entities;
using DevBlog.Repositories.Abstract;

namespace DevBlog.Repositories
{
    public class TagRepository : BaseRepository<Tag>
    {
        public TagRepository(BlogContext context)
            : base(context)
        { }

        public List<Tag> Get()
        {
            return Context.Tags.ToList();
        }
    }
}
