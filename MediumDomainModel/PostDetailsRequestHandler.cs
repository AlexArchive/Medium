using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace MediumDomainModel
{
    public class PostDetailsRequestHandler
    {
        public PostModel Handle(string postSlug)
        {
            var connectionStr = ConfigurationManager
                .ConnectionStrings["Medium"]
                .ConnectionString;
            using (var connection = new SqlConnection(connectionStr))
            {
                const string commandText = @"
                    SELECT *
                    FROM [Posts]
                    WHERE Slug = @slug";

                return connection.Query<PostModel>(commandText, new { slug = postSlug }).Single();
            }
        }
    }
}