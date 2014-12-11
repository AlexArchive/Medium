using System.Linq;
using Dapper;
using MediatR;

namespace MediumDomainModel
{
    public class PostRequestHandler : IRequestHandler<PostRequest, PostModel>
    {
        public PostModel Handle(PostRequest request)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                return connection.Query<PostModel>(
                    "SELECT * FROM [Posts] WHERE [Slug] = @slug", 
                    request)
                    .SingleOrDefault();
            }
        }
    }
}