using System.Collections.Generic;
using System.Linq;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class TagsRequestHandler : IRequestHandler<TagsRequest, TagsModel>
    {
        public TagsModel Handle(TagsRequest message)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var posts = connection.Query<PostModel>("SELECT * FROM [Posts]").ToList();
                foreach (var post in posts)
                {
                    post.Tags = connection.Query<TagModel>(@"
                        SELECT
                            [TagName] AS [Name],
                            (SELECT COUNT (*)
	                        FROM [Junction]
	                        WHERE [Junc].[TagName] = [TagName]) AS [Count]
                        FROM [Junction] AS [Junc]
                        WHERE [PostSlug] = @Slug", post);
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
}