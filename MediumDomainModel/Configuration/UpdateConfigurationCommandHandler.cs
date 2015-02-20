using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using MediatR;

namespace Medium.DomainModel.Configuration
{
    public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, Unit>
    {
        public Unit Handle(UpdateConfigurationCommand command)
        {
            var configPath = HttpContext.Current
                .Server
                .MapPath("~/Configuration.json");
            File.Delete(configPath);
            var configJson = new JavaScriptSerializer().Serialize(command.Configuration);
            File.WriteAllText(configPath, configJson);
            return Unit.Value;
        }
    }
}