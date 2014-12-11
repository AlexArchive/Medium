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
                connection.Execute(
                    "DELETE FROM [Posts] WHERE [Slug] = @Slug", 
                    command);
            }
            return Unit.Value;
        }
    }
}