using Dapper;
using System.Collections.Generic;
using MediatR;

namespace MediumDomainModel
{
    public class AllPostsRequestHandler : IRequestHandler<AllPostsRequest, IEnumerable<PostModel>>
    {
        public IEnumerable<PostModel> Handle(AllPostsRequest request)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                return connection.Query<PostModel>(
                    "SELECT * FROM [Posts]");
            }
        }
    }
}