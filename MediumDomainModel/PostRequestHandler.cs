using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Medium.DomainModel
{
    public class PostRequestHandler : IRequestHandler<PostRequest, PostModel>
    {
        private readonly IDbConnection _connection;

        public PostRequestHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public PostModel Handle(PostRequest request)
        {
            var param = new { Slug = request.PostSlug };
            var cache = new Dictionary<string, PostModel>();
            var command = @"
                SELECT 
	                P.*, 
	                J.TagName AS Name,
	                (SELECT COUNT (*) 
                     FROM dbo.PostTagJunction J1
                     WHERE J.TagName = J1.TagName) AS Count
                FROM dbo.Posts P
                LEFT OUTER JOIN dbo.PostTagJunction J
	                ON J.PostSlug = P.Slug
                WHERE Slug = @Slug";
            _connection.Query<PostModel, TagModel, PostModel>(command, (post, tag) =>
            {
                PostModel returnPost;
                if (!cache.TryGetValue(post.Slug, out returnPost))
                {
                    cache.Add(post.Slug, returnPost = post);
                }
                if (returnPost.Tags == null)
                {
                    returnPost.Tags = new List<TagModel>();
                }
                returnPost.Tags.Add(tag);
                return returnPost;
            }, param, splitOn: "Name");
            return cache.Values.Single();
        }
    }
}