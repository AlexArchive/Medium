using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace MediumDomainModel
{
    public class PostRequestHandler
    {
        public IEnumerable<PostModel> Handle()
        {
            var connectionStr = ConfigurationManager.ConnectionStrings["Medium"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT *
                        FROM [Posts]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new PostModel
                            {
                                Title = (string) reader["Title"],
                                Slug = (string) reader["Slug"]
                            };
                        }
                    }
                }
            }
        }
    }
}