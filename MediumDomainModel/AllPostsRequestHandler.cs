using System.Collections.Generic;
using System.Linq;
using Dapper;
using MediatR;

namespace MediumDomainModel
{
    public class AllPostsRequestHandler : IRequestHandler<AllPostsRequest, IEnumerable<PostModel>>
    {
        public IEnumerable<PostModel> Handle(AllPostsRequest request)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var posts = connection.Query<PostModel>(
                    "SELECT * FROM [Posts]");

                return request.IncludeDrafts 
                    ? posts 
                    : posts.Where(post => post.Published);
            }
        }
    }
}