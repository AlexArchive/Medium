using System.Linq;
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
                var posts = connection.Query<PostModel>("SELECT * FROM [Posts]");

                posts = request.IncludeDrafts
                    ? posts
                    : posts.Where(post => post.Published);

                return posts.ToPostPage(request.PageNumber, request.PostsPerPage);
            }
        }
    }
}