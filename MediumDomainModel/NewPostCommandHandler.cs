using Dapper;

namespace MediumDomainModel
{
    public class NewPostCommandHandler : ICommandHandler<PostModel>
    {
        public void Handle(PostModel command)
        {
            using (var connection = Database.Connect())
            {
                connection.Execute(
                    @"INSERT INTO [Posts] 
                      VALUES (@Slug, @Title, @Body, @Published, GETDATE())", 
                    command);
            }
        }
    }
}