using System.Data;
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

            _connection
                .Execute("DELETE FROM [PostTagJunction] WHERE [PostSlug] = @Slug", param);

            var deletedPosts = _connection
                .Execute("DELETE FROM [Posts] WHERE [Slug] = @Slug", param);

            return deletedPosts == 1;
        }
    }
}