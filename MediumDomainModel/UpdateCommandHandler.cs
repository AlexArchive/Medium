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
        private readonly IDbConnection _connection;

        public UpdateCommandHandler(IDbConnection connection)
        {
            _connection = connection;
        }

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

            _connection.Execute(@"
                INSERT INTO dbo.Posts
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
            _connection.Execute(@"
                DELETE FROM dbo.PostTagJunction
                WHERE PostSlug = @OriginalSlug", param);

            _connection.Execute(@"
                UPDATE dbo.Posts
                    SET Slug = @Slug, 
                        Title = @Title, 
                        Body = @Body, 
                        Published = @Published 
                WHERE Slug = @OriginalSlug", param);

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
                    _connection.Execute(@"
                        INSERT INTO dbo.Tags
                        VALUES (@Tag)", param);
                }

                var param1 = new { slug, tag };
                _connection.Execute(@"
                    INSERT INTO dbo.PostTagJunction
                    VALUES (@Slug, @Tag)", param1);
            }
        }

        private bool TagHasNotBeenSaved(string tagName)
        {
            var param = new { tagName };
            var record = _connection.ExecuteScalar(@"
                SELECT TOP 1 Name 
                FROM dbo.Tags 
                WHERE Name = @TagName", param);

            return record == null;
        }

        private bool SlugIsOccupied(string slug)
        {
            var param = new { slug };
            var record = _connection.ExecuteScalar(@"
                SELECT TOP 1 Slug 
                FROM dbo.Posts 
                WHERE Slug = @Slug", param);

            return record != null;
        }
    }
}