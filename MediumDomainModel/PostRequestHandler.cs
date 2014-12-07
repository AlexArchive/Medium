using Dapper;
using System.Collections.Generic;

namespace MediumDomainModel
{
    public class PostRequestHandler
    {
        public IEnumerable<PostModel> Handle()
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                return connection.Query<PostModel>(@"
                    SELECT *
                    FROM [Posts]");
            }
        }
    }
}