using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace MediumDomainModel
{
    public class PostRequestHandler
    {
        public IEnumerable<PostModel> Handle()
        {
            var connectionStr = ConfigurationManager
                .ConnectionStrings["Medium"]
                .ConnectionString;
            using (var connection = new SqlConnection(connectionStr))
            {
                const string commandText = @"
                    SELECT *
                    FROM [Posts]";
                return connection.Query<PostModel>(commandText);
            }
        }
    }
}