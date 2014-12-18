using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    public class TagsJsonController : Controller
    {
        private readonly IMediator requestBus;

        public TagsJsonController(IMediator requestBus)
        {
            this.requestBus = requestBus;
        }

        public JsonResult Search(TagsRequest request)
        {
            var model = requestBus.Send(request);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}