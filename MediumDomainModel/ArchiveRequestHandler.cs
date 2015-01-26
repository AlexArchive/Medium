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
                    Years =
                        from post in posts
                        group post by post.PublishedAt.Year
                        into postsByYear
                        from postsByMonth in
                            (from post in postsByYear
                                group post by post.PublishedAt.ToString("MMMM"))
                        group postsByMonth by postsByYear.Key
                };
            }
        }
    }
}