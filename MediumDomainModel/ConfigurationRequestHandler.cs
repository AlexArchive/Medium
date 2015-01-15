using System.Linq;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class ConfigurationRequestHandler : IRequestHandler<ConfigurationRequest, ConfigurationModel>
    {
        public ConfigurationModel Handle(ConfigurationRequest request)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var config = new ConfigurationModel
                {
                    BlogTitle = connection
                        .Query<string>("SELECT [Value] FROM [Configuration] WHERE [Key] = @key", new { key = "BlogTitle" })
                        .SingleOrDefault()
                };
                return config;
            }
        }
    }
}