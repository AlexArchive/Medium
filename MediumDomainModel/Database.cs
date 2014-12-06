using System.Configuration;
using System.Data.SqlClient;

namespace MediumDomainModel
{
    public class Database
    {
        public static SqlConnection Connect()
        {
            var connectionStr = ConfigurationManager
                .ConnectionStrings["Medium"]
                .ConnectionString;
            return new SqlConnection(connectionStr);
        } 
    }
}