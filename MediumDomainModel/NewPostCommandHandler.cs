using Dapper;

namespace MediumDomainModel
{
    public class NewPostCommandHandler : ICommandHandler<NewPostCommand, string>
    {
        public string Handle(NewPostCommand command)
        {
            using (var connection = Database.Connect())
            {
                var param = new
                {
                    Slug = command.Title.ToSlug(),
                    command.Title,
                    command.Body,
                    command.Published
                };

                connection.Execute(@"
                    INSERT INTO [Posts] 
                    VALUES (@Slug, @Title, @Body, @Published, GETDATE())", param);

                return param.Slug;
            }
        }
    }
}