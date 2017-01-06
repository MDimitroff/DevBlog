using System.Collections.Generic;

namespace DevBlog.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
