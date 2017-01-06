using System.Linq;
using DevBlog.Entities;
using DevBlog.Repositories;

namespace DevBlog.Services
{
    public class TagService
    {
        private readonly TagRepository _tagRepository;

        public TagService(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public int[] InsertTags(string[] tags)
        {
            var tagEntities = _tagRepository.Get();
            var allTags = tagEntities.Select(t => t.Name);
            var tagsToInsert = tags.Where(t => !allTags.Contains(t));

            foreach (var tag in tagsToInsert)
            {
                var tagEntity = new Tag
                {
                    Name = tag
                };

                _tagRepository.Create(tagEntity);
            }

            var result = _tagRepository.Get()
                .Where(t => tags.Contains(t.Name))
                .Select(t => t.Id)
                .ToArray();

            return result;
        }
    }
}
