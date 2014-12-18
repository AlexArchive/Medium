using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class AddPostCommandHandler : IRequestHandler<AddPostCommand, string>
    {
        public string Handle(AddPostCommand command)
        {
            var param = new
            {
                PublishDate = DateTime.Now,
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
                    "INSERT INTO [Posts] VALUES (@Slug, @Title, @Body, @Published, @PublishDate)",
                    param);

                IEnumerable<string> tags = TagConverter.Convert(command.Tags);
                foreach (var tag in tags)
                {
                    if (!TagExists(connection, tag))
                    {
                        connection.Execute(
                            "INSERT INTO [Tags] VALUES (@Tag)", new { tag });
                    }
                    connection.Execute(
                        "INSERT INTO [Junction] VALUES (@Slug, @Tag)", new { param.Slug, tag });
                }
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

        private static bool TagExists(IDbConnection connection, string tagName)
        {
            var param = new { tagName };
            var record = connection.ExecuteScalar(
                "SELECT TOP 1 [Name] FROM [Tags] WHERE [Name] = @TagName",
                param);
            return record != null;
        }
    }
}