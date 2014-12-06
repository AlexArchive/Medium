using Dapper;

namespace MediumDomainModel
{
    public class EditPostCommandHandler : ICommandHandler<PostModel>
    {
        public void Handle(PostModel post)
        {
            using (var connection = Database.Connect())
            {
                connection.Execute(@"
                    UPDATE 
                        [Posts]
                    SET 
                        [Title] = @Title,
                        [Body] = @Body,
                        [Published] = @Published
                    WHERE [Slug] = @Slug", post);
            }
        }
    }
}