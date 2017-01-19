using Nest;

namespace DevBlog.Models
{
    [ElasticsearchType(Name = "post")]
    public class PostType
    {
        [Number(NumberType.Integer)] 
        public int Id { get; set; }

        [Text]
        public string Title { get; set; }

        [Text]
        public string Content { get; set; }

        [Text]
        public string Tags { get; set; }

        [Boolean(Store = false)]
        public bool Deleted { get; set; }
    }
}
