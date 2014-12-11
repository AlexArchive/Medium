using System.Web.Mvc;
using MediatR;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class PostController : Controller
    {
        private readonly IMediator requestBus;

        public PostController(IMediator requestBus)
        {
            this.requestBus = requestBus;
        }

        public ActionResult Index(PostRequest request)
        {
            var model = requestBus.Send(request);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}