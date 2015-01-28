using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Medium.DomainModel
{
    public class SqlConnectionFactory
    {
        public static IDbConnection Create()
        {
            var connectionStr = ConfigurationManager
                .ConnectionStrings["Medium"]
                .ConnectionString;

            // You have to use the DbProviderFactories class or else Glimpse won't collect data for the Sql tab!
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionStr;
            return connection;
        }
    }
}