using System.Web.Mvc;
using MediatR;
using Medium.DomainModel.Configuration;

namespace Medium.WebModel.Controllers
{
    public class ControllerBase : Controller
    {
        protected IMediator Mediator;

        public ControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        private ConfigurationModel _configurationModel;
        protected ConfigurationModel Configuration
        {
            get
            {
                if (_configurationModel != null)
                {
                    return _configurationModel;
                }

                _configurationModel = Mediator.Send(new ConfigurationRequest());
                return _configurationModel;
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            ViewBag.SiteConfig = Configuration;
        }
    }
}