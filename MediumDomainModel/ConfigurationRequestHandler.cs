using MediatR;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Medium.DomainModel
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