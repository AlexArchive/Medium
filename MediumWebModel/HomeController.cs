using System.Web.Mvc;
using MediatR;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class HomeController : Controller
    {
        private readonly IMediator bus;

        public HomeController(IMediator bus)
        {
            this.bus = bus;
        }

        public ActionResult Index()
        {
            var request = new AllPostsRequest { IncludeDrafts = false };
            var model = bus.Send(request);
            return View(model);
        }
    }
}