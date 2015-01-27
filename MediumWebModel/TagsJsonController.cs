using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    public class TagsJsonController : ControllerBase
    {
        public TagsJsonController(IMediator mediator) 
            : base(mediator)
        {
        }

        public JsonResult Search(TagSequenceRequest request)
        {
            var model = base.Mediator.Send(request);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}