using Dapper;
using MediatR;

namespace MediumDomainModel
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
    {
        public Unit Handle(DeletePostCommand command)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var param = new { Slug = command.PostSlug };

                connection.Execute("DELETE FROM [Posts] WHERE [Slug] = @Slug", param);

                return Unit.Value;
            }
        }
    }
}