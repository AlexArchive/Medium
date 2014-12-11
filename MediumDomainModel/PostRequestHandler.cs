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
                var param = new {Slug = request.PostSlug};

                var post =
                    connection.Query<PostModel>(
                        "SELECT * FROM [Posts] WHERE [Slug] = @Slug",
                        param)
                        .SingleOrDefault();

                return post;
            }
        }
    }
}