using System.ComponentModel.DataAnnotations.Schema;

namespace DevBlog.Entities
{
    public class PostTag
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int TagId { get; set; }

        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }

        [ForeignKey(nameof(TagId))]
        public virtual Tag Tag { get; set; }
    }
}
