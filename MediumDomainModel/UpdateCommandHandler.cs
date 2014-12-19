using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class UpdateCommandHandler :
        IRequestHandler<AddPostCommand, string>,
        IRequestHandler<EditPostCommand, string>
    {
        private readonly IDbConnection connection = SqlConnectionFactory.Create();

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

            if (SlugIsOccupied(param.Slug))
            {
                return null;
            }

            connection.Execute(@"
                INSERT INTO [Posts] 
                VALUES (@Slug, @Title, @Body, @Published, @PublishDate)", param);

            UpdateTags(command.Tags, param.Slug);

            return param.Slug;
        }

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

            var slugHasBeenEdited = param.OriginalSlug != param.Slug;
            if (slugHasBeenEdited && SlugIsOccupied(param.Slug))
            {
                return null;
            }
            connection.Execute(@"
                DELETE FROM [Junction]
                WHERE [PostSlug] = @OriginalSlug", param);

            connection.Execute(@"
                UPDATE [Posts]
                    SET [Slug] = @Slug, 
                        [Title] = @Title, 
                        [Body] = @Body, 
                        [Published] = @Published 
                WHERE [Slug] = @OriginalSlug", param);

            UpdateTags(command.Tags, param.Slug);

            return param.Slug;
        }

        private void UpdateTags(string delimitedTags, string slug)
        {
            IEnumerable<string> tags = TagConverter.Convert(delimitedTags);

            foreach (var tag in tags)
            {
                if (TagHasNotBeenSaved(tag))
                {
                    var param = new { tag };
                    connection.Execute(@"
                        INSERT INTO [Tags] 
                        VALUES (@Tag)", param);
                }

                var param1 = new { slug, tag };
                connection.Execute(@"
                    INSERT INTO [Junction] 
                    VALUES (@Slug, @Tag)", param1);
            }
        }

        private bool TagHasNotBeenSaved(string tagName)
        {
            var param = new { tagName };
            var record = connection.ExecuteScalar(@"
                SELECT TOP 1 [Name] 
                FROM [Tags] 
                WHERE [Name] = @TagName", param);

            return record == null;
        }

        private bool SlugIsOccupied(string slug)
        {
            var param = new { slug };
            var record = connection.ExecuteScalar(@"
                SELECT TOP 1 [Slug] 
                FROM [Posts] 
                WHERE [Slug] = @Slug", param);

            return record != null;
        }
    }
}