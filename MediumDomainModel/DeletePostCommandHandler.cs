using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IDbConnection _connection;

        public DeletePostCommandHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public bool Handle(DeletePostCommand command)
        {
            var param = new { Slug = command.PostSlug };

            var tags = _connection
                .Query<string>("SELECT TagName FROM dbo.PostTagJunction WHERE PostSlug=@Slug", param);

            _connection
                .Execute("DELETE FROM dbo.PostTagJunction WHERE PostSlug = @Slug", param);

            var successful = _connection
                .Execute("DELETE FROM dbo.Posts WHERE Slug = @Slug", param) == 1;

            foreach (var tag in tags)
            {
                var param2 = new { Tag = tag };
                _connection
                    .Execute(@"
                        IF NOT EXISTS(SELECT 1 FROM dbo.PostTagJunction WHERE TagName = @Tag)
                        DELETE FROM dbo.Tags WHERE Name= @Tag", param2);
            }

            return successful;
        }
    }
}