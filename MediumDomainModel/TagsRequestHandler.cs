using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class TagsRequestHandler : IRequestHandler<TagsRequest, TagsModel>
    {
        private readonly IDbConnection _connection;

        public TagsRequestHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public TagsModel Handle(TagsRequest message)
        {
            var posts = _connection.Query<PostModel>("SELECT * FROM dbo.Posts").ToList();
            foreach (var post in posts)
            {
                post.Tags = _connection.Query<TagModel>(@"
                        SELECT
                            TagName AS Name,
                            (SELECT COUNT (*)
	                        FROM dbo.PostTagJunction
	                        WHERE Junc.TagName = TagName) AS Count
                        FROM dbo.PostTagJunction AS Junc
                        WHERE PostSlug = @Slug", post);
            }
            return new TagsModel
            {
                Tags = posts.SelectMany(post => post.Tags).Distinct(),

                TagsMap = posts
                    .SelectMany(post => post.Tags.Select(tag => new { Tag = tag, Post = post }))
                    .GroupBy(pair => pair.Tag)
                    .ToDictionary(group => group.Key, group => group.Select(pair => pair.Post).ToArray())
            };
        }
    }
}