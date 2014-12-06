using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace MediumDomainModel
{
    public class NewPostHandler
    {
        public void Handle(PostModel post)
        {
            var connectionStr = ConfigurationManager
                .ConnectionStrings["Medium"]
                .ConnectionString;
            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Execute(
                    @"INSERT INTO [Posts] 
                      VALUES (@Slug, @Title, @Body, @Published, GETDATE())", 
                    post);
            }
        }
    }
}