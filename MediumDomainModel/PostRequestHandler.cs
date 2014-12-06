using Dapper;
using System.Collections.Generic;

namespace MediumDomainModel
{
    public class PostRequestHandler
    {
        public IEnumerable<PostModel> Handle()
        {
            using (var connection = Database.Connect())
            {
                const string commandText = @"
                    SELECT *
                    FROM [Posts]";
                return connection.Query<PostModel>(commandText);
            }
        }
    }
}