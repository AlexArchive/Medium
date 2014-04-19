using System.Diagnostics;
using System.Net;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            ViewBag.FriendlyErrorMessage = "The page you were looking for could not be found.";
            return View("Index");
        }

        public ActionResult ServerError()
        {
            var foo = Server.GetLastError();
            Debugger.Break();
            
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            ViewBag.FriendlyErrorMessage = "An internal server error occurred. Please try again later.";
            return View("Index");
        }
    }
}