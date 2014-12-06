using Dapper;

namespace MediumDomainModel
{
    public class DeleteCommandHandler
    {
        public void Handle(string postSlug)
        {
            using (var connection = Database.Connect())
            {
                connection.Execute("DELETE FROM Posts WHERE Slug= @Slug", new { slug = postSlug });
            }
        }
    }
}