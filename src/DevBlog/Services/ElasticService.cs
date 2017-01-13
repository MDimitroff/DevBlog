using DevBlog.Models;
using DevBlog.Services.Elasticsearch;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevBlog.Services
{
    public class ElasticService
    {
        private readonly ElasticProvider _elastic;

        public ElasticService(ElasticProvider elastic)
        {
            _elastic = elastic;
        }

        public void IndexData(PostModel postModel)
        {
            var post = new PostType
            {
                Id = postModel.Id,
                Title = postModel.Title,
                Content = postModel.Content,
                Tags = postModel.Tags,
                Deleted = postModel.Deleted
            };

            _elastic.InsertUpdate(post);
        }

        public List<PostModel> Search(string terms)
        {
            var splittedTerms = terms.Split(' ');
            var response = _elastic.Search(splittedTerms);

            string dslQuery = Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes);

            var result = response.Hits
                .Select(p => new PostModel
                {
                    Id = p.Source.Id,
                    Title = p.Source.Title,
                    Content = p.Source.Content,
                    TagNames = p.Source.Tags.Split(',').ToList()
                })
                .ToList();

            result.ForEach((post) => 
            {
                if (post.Content.Length > 400)
                {
                    post.Content = post.Content.Substring(0, 400) + "...";
                }
            });

            return result;
        }
    }
}
