using System.Collections.Generic;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class AllPostsRequestHandler : IRequestHandler<AllPostsRequest, IEnumerable<PostModel>>
    {
        public IEnumerable<PostModel> Handle(AllPostsRequest message)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var posts = connection.Query<PostModel>(
                    "SELECT * FROM [Posts]");

                foreach (var post in posts)
                {
                    post.Tags = connection
                        .Query<TagModel>(@"
                        SELECT 
                            [TagName] AS [Name],
                            (SELECT COUNT (*) 
	                         FROM [Junction]
	                          WHERE [Junc].[TagName] = [TagName]) AS [Count]
                        FROM [Junction] AS [Junc]
                        WHERE [PostSlug] = @Slug", post);
                }

                return posts;
            }
        }
    }
}