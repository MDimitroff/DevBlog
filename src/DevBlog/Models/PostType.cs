using Nest;
using System.Collections.Generic;

namespace DevBlog.Models
{
    [ElasticsearchType(Name = "post")]
    public class PostType
    {
        [Number]
        public int Id { get; set; }

        [String]
        public string Title { get; set; }

        [String]
        public string Content { get; set; }

        [String]
        public string Tags { get; set; }

        [Boolean(Store = false)]
        public bool Deleted { get; set; }
    }
}
