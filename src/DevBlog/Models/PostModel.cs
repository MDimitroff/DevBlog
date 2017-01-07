using System.Collections.Generic;

namespace DevBlog.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public List<string> TagNames { get; set; }
    }
}
