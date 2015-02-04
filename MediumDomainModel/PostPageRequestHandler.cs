using System.Collections.Generic;
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using UniqueNamespace.Dapper;

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
            var param = new
            {
                Start = (request.PageNumber - 1)*request.PostsPerPage + 1,
                Finish = request.PageNumber*request.PostsPerPage
            };
            var builder = new SqlBuilder();
            var countSql = builder.AddTemplate("SELECT COUNT(*) FROM dbo.Posts {{WHERE}}");
            var postsSql = builder.AddTemplate(@"
                SELECT * FROM
                    (SELECT *, ROW_NUMBER() OVER (ORDER BY PublishedAt DESC) AS RowNum
                     FROM dbo.Posts
                     {{WHERE}}) 
                    As RowConstrainedResult
                WHERE RowNum BETWEEN @Start AND @Finish", param);
            if (!request.IncludeDrafts)
            {
                builder.Where("Published = 1");
            }
            int count = _connection.Query<int>(countSql.RawSql).Single();
            IEnumerable<PostModel> posts = _connection.Query<PostModel>(
                postsSql.RawSql, postsSql.Parameters);
            return new PostPage(posts, request.PageNumber, count, request.PostsPerPage);
        }
    }
}