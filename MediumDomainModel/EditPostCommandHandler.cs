using Dapper;
using Medium.WebModel;

namespace MediumDomainModel
{
    public class EditPostCommand
    {
        public string Slug { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
    }

    public class EditPostCommandHandler : ICommandHandler<EditPostCommand, string>
    {
        public string Handle(EditPostCommand command)
        {
            var o = new
            {
                OriginalSlug = command.Slug,
                Slug = command.Title.ToSlug(),
                command.Title,
                command.Body,
                command.Published
            };

            using (var connection = Database.Connect())
            {
                connection.Execute(@"
                    UPDATE 
                        [Posts]
                    SET 
                        [Slug] = @Slug,
                        [Title] = @Title,
                        [Body] = @Body,
                        [Published] = @Published
                    WHERE [Slug] = @OriginalSlug", o);
            }

            return o.Slug;
        }
    }
}