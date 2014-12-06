using Dapper;

namespace MediumDomainModel
{
    public class EditPostCommandHandler : ICommandHandler<EditPostCommand, string>
    {
        public string Handle(EditPostCommand command)
        {
            var param = new
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
                    WHERE [Slug] = @OriginalSlug", param);
            }

            return param.Slug;
        }
    }
}