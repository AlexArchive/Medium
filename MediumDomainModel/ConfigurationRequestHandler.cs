using System.Data;
using System.Linq;
using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class ConfigurationRequestHandler : IRequestHandler<ConfigurationRequest, ConfigurationModel>
    {
        private readonly IDbConnection _connection;

        public ConfigurationRequestHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public ConfigurationModel Handle(ConfigurationRequest request)
        {
            var config = new ConfigurationModel
            {
                BlogTitle = _connection
                    .Query<string>("SELECT [Value] FROM [Configuration] WHERE [Key] = @key", new {key = "BlogTitle"})
                    .SingleOrDefault()
            };
            return config;
        }
    }
}