using System.Web.Mvc;

namespace GoBlog.Controllers
{
    public class HomeController  : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}