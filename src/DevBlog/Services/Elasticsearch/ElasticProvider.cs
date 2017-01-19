using DevBlog.Models;
using Nest;
using System;

namespace DevBlog.Services.Elasticsearch
{
    public class ElasticProvider
    {
        private const string _indexName = "blog";
        public static IElasticClient _elastic { get; set; }

        public static void Initialize()
        {
            _elastic = GetClient();

            // Create index and define the custom filters and analyzers
            _elastic.CreateIndex(_indexName, i => i
                .Settings(s => s
                    .Setting("number_of_shards", 1)
                    .Setting("number_of_replicas", 0)));

            // Declaration of index's mappings
            _elastic.Map<PostType>(x => x
                .Index(_indexName)
                .AutoMap());
        }

        /// <summary>
        /// Inserts or updates the entity in elastic search.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public void InsertUpdate(PostType document)
        {
            if(document.Deleted)
            {
                _elastic.Delete<PostType>(document.Id.ToString(), i => i
                     .Index(_indexName));
            }
            else
            {
                _elastic.Index(document, i => i
                    .Id(document.Id.ToString())
                    .Index(_indexName)
                    .Type<PostType>());
            }
        }

        public ISearchResponse<PostType> Search(string[] terms)
        {
            var result = _elastic.Search<PostType>(s => s
                .Index(_indexName)
                .Type<PostType>()
                .Query(q => MakeQuery(terms)));

            return result;
        }

        private static IElasticClient GetClient()
        {
            var urlString = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(urlString).DisableDirectStreaming();

            return new Nest.ElasticClient(settings);
        }

        private QueryContainer MakeQuery(string[] terms)
        {
            QueryContainer query = null;

            foreach (var term in terms)
            {
                query |= Query<PostType>.MultiMatch(mm => mm
                    .Query(term)
                    .Type(TextQueryType.MostFields)
                    .Fields(f => f
                        .Field(ff => ff.Title, boost: 8)
                        .Field(ff => ff.Content, boost: 1)
                        .Field(ff => ff.Tags, boost: 2)));
            }

            return query;
        }
    }
}
