using System.Data;
using System.Linq;
using Dapper;
using MediatR;

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
            var posts = _connection.Query<PostModel>(
                "SELECT * FROM [Posts] ORDER BY [PublishedAt] DESC");

            posts = request.IncludeDrafts
                ? posts
                : posts.Where(post => post.Published);

            return posts.ToPostPage(request.PageNumber, request.PostsPerPage);
        }
    }
}