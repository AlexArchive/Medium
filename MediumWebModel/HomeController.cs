using MediumDomainModel;
using System.Web.Mvc;

namespace Medium.WebModel
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var requestHandler = new AllPostsRequestHandler();
            var request = new AllPostsRequest();
            var model = requestHandler.Handle(request);
            return View(model);
        }
    }
}