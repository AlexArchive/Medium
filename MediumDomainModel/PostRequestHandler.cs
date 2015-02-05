using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MediatR;

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
	                dbo.Posts.*, 
	                Slug as Name,
	                (SELECT COUNT (*) 
                     FROM dbo.PostTagJunction
                     WHERE dbo.PostTagJunction.TagName = TagName) AS Count
                FROM dbo.Posts
                LEFT OUTER JOIN dbo.PostTagJunction ON dbo.PostTagJunction.PostSlug = dbo.Posts.Slug
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