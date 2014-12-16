using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class PostPageRequestHandler : IRequestHandler<PostPageRequest, PostPage>
    {
        public PostPage Handle(PostPageRequest request)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var posts = connection.Query<PostModel>(
                    "SELECT * FROM [Posts] WHERE [Published] = 1");
                return posts.ToPostPage(
                    request.PageNumber, request.PostsPerPage);
            }
        }
    }
}