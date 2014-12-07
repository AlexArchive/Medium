using Dapper;

namespace MediumDomainModel
{
    public class AddPostCommandHandler : ICommandHandler<AddPostCommand, string>
    {
        public string Handle(AddPostCommand command)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var param = new
                {
                    Slug = command.Title.ToSlug(),
                    command.Title,
                    command.Body,
                    command.Published
                };
                connection.Execute(
                    "INSERT INTO [Posts] VALUES (@Slug, @Title, @Body, @Published, GETDATE())", 
                    param);
                return param.Slug;
            }
        }
    }
}