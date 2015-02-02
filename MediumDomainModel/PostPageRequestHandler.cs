using Dapper;
using MediatR;
using System.Data;
using System.Text;

namespace Medium.DomainModel
{
    public class PostPageRequestHandler : IRequestHandler<PostPageRequest, PostPage>
    {
        private readonly IDbConnection _connection;

        public PostPageRequestHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public PostPage Handle(PostPageRequest request)
        {
            var builder = new StringBuilder();
            builder.Append("SELECT * FROM [Posts]");
            if (!request.IncludeDrafts)
                builder.Append("WHERE [Published] = 1");
            builder.Append("ORDER BY [PublishedAt] DESC");
            var posts = _connection.Query<PostModel>(builder.ToString());
            return posts.ToPostPage(request.PageNumber, request.PostsPerPage);
        }
    }
}