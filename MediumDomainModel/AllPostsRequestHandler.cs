using Dapper;
using System.Collections.Generic;

namespace MediumDomainModel
{
    public class AllPostsRequestHandler : MediatR.IRequestHandler<AllPostsRequest, IEnumerable<PostModel>>
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