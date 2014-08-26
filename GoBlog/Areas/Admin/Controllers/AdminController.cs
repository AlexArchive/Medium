using System.Web.Mvc;

namespace GoBlog.Areas.Admin.Controllers
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