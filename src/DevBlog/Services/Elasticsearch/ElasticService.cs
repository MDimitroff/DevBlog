using DevBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevBlog.Services.Elasticsearch
{
    public class ElasticService
    {
        private readonly ElasticClient _elastic;

        public ElasticService(ElasticClient elastic)
        {
            _elastic = elastic;
        }

        public void Index(PostModel postModel)
        {
            var splittedTags = postModel.Tags
                .ToLower()
                .Split(',');

            var tags = new List<string>();
            foreach (var tag in splittedTags)
            {
                tags.Add(tag.Trim());
            }
            
            var post = new PostType
            {
                Id = postModel.Id,
                Title = postModel.Title,
                Content = postModel.Content,
                Tags = tags,
                Deleted = postModel.Deleted
            };

            _elastic.Index(post);
        }

        public void Delete(int id)
        {
            _elastic.Delete(id);
        }

        public List<PostModel> Search(string terms)
        {
            if (string.IsNullOrEmpty(terms))
            {
                return new List<PostModel>();
            }

            var response = _elastic.Search(terms);

            string dslQuery = Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes);

            var result = response.Hits
                .Select(p => new PostModel
                {
                    Id = p.Source.Id,
                    Title = p.Source.Title,
                    Content = p.Source.Content,
                    TagNames = p.Source.Tags
                })
                .ToList();

            return result;
        }
    }
}
