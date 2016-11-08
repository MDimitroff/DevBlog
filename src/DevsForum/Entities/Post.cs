using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevsForum.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
