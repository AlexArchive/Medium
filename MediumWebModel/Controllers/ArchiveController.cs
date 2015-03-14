using System.Web.Mvc;
using MediatR;
using Medium.DomainModel.Archive;

namespace Medium.WebModel.Controllers
{
    public class ArchiveController : ControllerBase
    {
        public ArchiveController(IMediator mediator) : base(mediator)
        {
        }

        public ActionResult Index()
        {
            var model = base.Mediator.Send(new ArchiveRequest());
            return View(model);
        }
    }
}