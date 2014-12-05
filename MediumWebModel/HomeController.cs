using System.Web.Mvc;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var database = new PostRequestHandler();
            var model = database.Handle();

            return View(model);
        }
    }
}