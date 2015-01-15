using MediatR;
using Medium.DomainModel;
using System.Web.Mvc;

namespace Medium.WebModel
{
    public class ControllerBase : Controller
    {
        protected IMediator Mediator;

        public ControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            ViewBag.SiteConfig = Mediator.Send(new ConfigurationRequest());
        }
    }
}