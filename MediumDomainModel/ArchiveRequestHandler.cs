using System.Data;
using System.Linq;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class ArchiveRequestHandler : IRequestHandler<ArchiveRequest, ArchiveModel>
    {
        private readonly IDbConnection _connection;

        public ArchiveRequestHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public ArchiveModel Handle(ArchiveRequest message)
        {
            var posts = _connection.Query<PostModel>("SELECT * FROM [Posts]");
            return new ArchiveModel
            {
                Years =
                    from post in posts
                    orderby post.PublishedAt descending
                    where post.Published
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