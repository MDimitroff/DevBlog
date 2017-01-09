using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevBlog.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Tags { get; set; }

        public List<string> TagNames { get; set; }

        public bool Deleted { get; set; }
    }
}
