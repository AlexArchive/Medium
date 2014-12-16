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

        public ActionResult Index(int pageNumber = 1)
        {
            var postPageRequest = new PostPageRequest(pageNumber, postsPerPage: 2);
            var postPage = bus.Send(postPageRequest);
            return View(postPage);
        }
    }
}