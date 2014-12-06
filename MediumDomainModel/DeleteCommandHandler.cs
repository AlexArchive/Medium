using Dapper;

namespace MediumDomainModel
{
    public class DeleteCommand
    {
        public string PostSlug { get; set; }
    }

    public class DeleteCommandHandler : ICommandHandler<DeleteCommand>
    {
        public void Handle(DeleteCommand command)
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