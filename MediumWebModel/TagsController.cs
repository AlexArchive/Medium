using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    public class TagsController : ControllerBase
    {
        public TagsController(IMediator mediator)
            : base(mediator)
        {
        }

        public ActionResult Index()
        {
            var model = base.Mediator.Send(new TagsRequest());
            return View(model);
        }
    }
}