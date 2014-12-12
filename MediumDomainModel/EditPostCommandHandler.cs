using System.Data;
using Dapper;
using MediatR;

namespace MediumDomainModel
{
    public class EditPostCommandHandler : IRequestHandler<EditPostCommand, string>
    {
        public string Handle(EditPostCommand command)
        {
            var param = new
            {
                Slug = SlugConverter.Convert(command.Title),
                command.OriginalSlug,
                command.Title,
                command.Body,
                command.Published
            };

            using (var connection = SqlConnectionFactory.Create())
            {
                if (param.OriginalSlug != param.Slug && 
                    SlugTaken(connection, param.Slug))
                {
                    return null;
                }

                connection.Execute(@"
                    UPDATE [Posts]
                        SET [Slug] = @Slug, 
                            [Title] = @Title, 
                            [Body] = @Body, 
                            [Published] = @Published 
                    WHERE [Slug] = @OriginalSlug",
                    param);
            }

            return param.Slug;
        }

        private static bool SlugTaken(IDbConnection connection, string slug)
        {
            var param = new { slug };
            var record = connection.ExecuteScalar(
                "SELECT TOP 1 [Slug] FROM [Posts] WHERE [Slug] = @Slug",
                param);

            return record != null;
        }
    }
}