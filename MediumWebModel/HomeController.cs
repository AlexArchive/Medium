using System.Web.Mvc;
using MediatR;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class HomeController : Controller
    {
        private readonly IMediator requestBus;

        public HomeController(IMediator requestBus)
        {
            this.requestBus = requestBus;
        }

        public ActionResult Index()
        {
            var request = new AllPostsRequest(includeDrafts: false);
            var model = requestBus.Send(request);
            return View(model);
        }
    }
}