using Dapper;

namespace MediumDomainModel
{
    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
    {
        public void Handle(DeletePostCommand command)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                connection.Execute(
                    "DELETE FROM [Posts] WHERE [Slug] = @Slug", 
                    command);
            }
        }
    }
}