using System.Data;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IDbConnection connection;

        public DeletePostCommandHandler(IDbConnection connection)
        {
            this.connection = connection;
        }

        public bool Handle(DeletePostCommand command)
        {
            var param = new { Slug = command.PostSlug };

            connection.Execute(@"
                DELETE 
                FROM dbo.PostTagJunction 
                WHERE PostSlug = @Slug", param);

            var successful = connection.Execute(@"
                DELETE 
                FROM dbo.Posts 
                WHERE Slug = @Slug", param) == 1;

            connection.Execute(@"
                DELETE 
                FROM dbo.Tags
                WHERE NOT EXISTS (
                    SELECT * 
                    FROM dbo.PostTagJunction 
                    WHERE PostTagJunction.TagName = Tags.Name
                )");

            return successful;
        }
    }
}