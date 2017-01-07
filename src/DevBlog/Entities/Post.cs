using System.Collections.Generic;

namespace DevBlog.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<PostTag> Tags { get; set; }
    }
}
