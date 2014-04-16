using System.Net;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return View();
        }
    }
}