using System.Web.Mvc;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}