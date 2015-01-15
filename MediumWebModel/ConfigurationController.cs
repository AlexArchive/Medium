using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    public class ConfigurationController : ControllerBase
    {
        public ConfigurationController(IMediator mediator) : base(mediator)
        {
        }

        public ActionResult Index()
        {
            var configuration = base.Mediator.Send(new ConfigurationRequest());
            return View(configuration);
        }

        public ActionResult Save(ConfigurationModel configuration)
        {
            base.Mediator.Send(new UpdateConfigurationCommand { Configuration = configuration });
            return View("Index");
        }
    }
}