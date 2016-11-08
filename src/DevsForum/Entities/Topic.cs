using System.Collections.Generic;

namespace DevsForum.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }

        public List<Post> Posts { get; set; }
    }
}
