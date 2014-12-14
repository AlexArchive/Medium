using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

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
            var posts = bus.Send(request);
            return View(posts);
        }
    }
}