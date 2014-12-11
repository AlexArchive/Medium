using Dapper;
using MediatR;

namespace MediumDomainModel
{
    public class AddPostCommandHandler : IRequestHandler<AddPostCommand, string>
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