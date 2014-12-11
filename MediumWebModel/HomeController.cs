using System.Linq;
using MediumDomainModel;
using System.Web.Mvc;
using MediatR;

namespace Medium.WebModel
{
    public class HomeController : Controller
    {
        private readonly IMediator requestBroker;

        public HomeController(IMediator requestBroker)
        {
            this.requestBroker = requestBroker;
        }

        public ActionResult Index()
        {
            //var requestHandler = new AllPostsRequestHandler();
            var request = new AllPostsRequest();
            var model = requestBroker.Send(request)
                .Where(post => post.Published);
            return View(model);
        }
    }
}