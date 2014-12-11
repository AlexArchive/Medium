using System.Data;
using Dapper;
using MediatR;

namespace MediumDomainModel
{
    public class AddPostCommandHandler : IRequestHandler<AddPostCommand, Response<string>>
    {
        public Response<string> Handle(AddPostCommand command)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var postSlug = SlugConverter.Convert(command.Title);

                if (UniqueKeyOccupied(connection, postSlug))
                {
                    return Response<string>.UnsuccessfulResponse;
                }

                var param = new
                {
                    Slug = SlugConverter.Convert(command.Title),
                    command.Title,
                    command.Body,
                    command.Published
                };

                connection.Execute(
                    "INSERT INTO [Posts] VALUES (@Slug, @Title, @Body, @Published, GETDATE())",
                    param);

                return new Response<string>() {Data = param.Slug};
            }
        }

        private static bool UniqueKeyOccupied(IDbConnection connection, string slug)
        {
            var param = new { Slug = slug };
            var record = connection.ExecuteScalar(
                "SELECT TOP 1 [Slug] FROM [Posts] WHERE [Slug] = @Slug", 
                param);

            return record != null;
        }
    }
}