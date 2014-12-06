using Dapper;

namespace MediumDomainModel
{
    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
    {
        public void Handle(DeletePostCommand command)
        {
            using (var connection = Database.Connect())
            {
                connection.Execute(
                    @"DELETE FROM [Posts] WHERE [Slug] = @Slug", 
                    new { slug = command.PostSlug });
            }
        }
    }
}