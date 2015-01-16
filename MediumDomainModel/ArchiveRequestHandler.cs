using System.Linq;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class ArchiveRequestHandler : IRequestHandler<ArchiveRequest, ArchiveModel>
    {
        public ArchiveModel Handle(ArchiveRequest message)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var posts = connection.Query<PostModel>("SELECT * FROM [Posts]");
                return new ArchiveModel
                {
                    Years = posts.GroupBy(post => post.PublishedAt.Year)
                };
            }
        }
    }
}