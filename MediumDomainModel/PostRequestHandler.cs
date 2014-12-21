using System.Linq;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class PostRequestHandler : IRequestHandler<PostRequest, PostModel>
    {
        public PostModel Handle(PostRequest request)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var param = new { Slug = request.PostSlug };

                var post = connection
                    .Query<PostModel>(@"SELECT * FROM [Posts] WHERE [Slug] = @Slug", param)
                    .SingleOrDefault();

                post.Tags = connection
                    .Query<TagModel>(@"
                        SELECT 
                            [TagName] AS [Name],
                            (SELECT COUNT (*) 
	                         FROM [Junction]
	                          WHERE [Junc].[TagName] = [TagName]) AS [Count]
                        FROM [Junction] AS [Junc]
                        WHERE [PostSlug] = @Slug", param);

                return post;
            }
        }
    }
}