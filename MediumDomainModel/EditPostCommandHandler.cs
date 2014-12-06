using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace MediumDomainModel
{
    public class EditPostCommandHandler
    {
        public void Handle(PostModel post)
        {
            var connectionStr = ConfigurationManager
                .ConnectionStrings["Medium"]
                .ConnectionString;
            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Execute(
                    @"UPDATE [Posts]
                        SET Title = @Title,
                            Body = @Body,
                            Published = @Published
                        WHERE Slug = @Slug", post);
            }
        }
    }
}