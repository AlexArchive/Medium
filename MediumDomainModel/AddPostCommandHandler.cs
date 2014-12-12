using System.Data;
using Dapper;
using MediatR;

namespace MediumDomainModel
{
    public class AddPostCommandHandler : IRequestHandler<AddPostCommand, string>
    {
        public string Handle(AddPostCommand command)
        {
            var param = new
            {
                Slug = SlugConverter.Convert(command.Title),
                command.Title,
                command.Body,
                command.Published
            };

            using (var connection = SqlConnectionFactory.Create())
            {
                if (SlugTaken(connection, param.Slug))
                {
                    return null;
                }

                connection.Execute(
                    "INSERT INTO [Posts] VALUES (@Slug, @Title, @Body, @Published, GETDATE())",
                    param);

                return param.Slug;
            }
        }

        private static bool SlugTaken(IDbConnection connection, string slug)
        {
            var param = new { Slug = slug };
            var record = connection.ExecuteScalar(
                "SELECT TOP 1 [Slug] FROM [Posts] WHERE [Slug] = @Slug", 
                param);

            return record != null;
        }
    }
}