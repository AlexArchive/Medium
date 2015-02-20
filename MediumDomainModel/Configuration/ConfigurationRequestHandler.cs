using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using MediatR;

namespace Medium.DomainModel.Configuration
{
    public class ConfigurationRequestHandler : IRequestHandler<ConfigurationRequest, ConfigurationModel>
    {
        public ConfigurationModel Handle(ConfigurationRequest request)
        {
            var configPath = HttpContext.Current
                .Server
                .MapPath("~/Configuration.json");
            var configJson = File.ReadAllText(configPath);
            var config = new JavaScriptSerializer().Deserialize<ConfigurationModel>(configJson);
            return config;
        }
    }
}