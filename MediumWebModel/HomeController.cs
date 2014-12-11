using System.Linq;
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
            var request = new AllPostsRequest();
            var model = requestBus
                .Send(request)
                .Where(post => post.Published);
            return View(model);
        }
    }
}