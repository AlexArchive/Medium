using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}