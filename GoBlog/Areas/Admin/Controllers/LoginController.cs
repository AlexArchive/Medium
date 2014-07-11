using System.Web.Mvc;
using GoBlog.Areas.Admin.Models;
using GoBlog.Authentication;

namespace GoBlog.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService authService;

        public LoginController(IAuthenticationService authService)
        {
            this.authService = authService;
        }

        public ActionResult Index()
        {
            if (authService.Authenticated)
            {
                return Redirect("/admin");
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Authenticate(CredentialsModel credentials)
        {
            if (ModelState.IsValid)
            {
                var authenticated = authService.Authenticate(credentials.Username, credentials.Password);
                if (authenticated)
                {
                    return Redirect(Request.QueryString["ReturnUrl"] ?? "/admin");
                }
                ModelState.AddModelError("", "Username or Password is incorrect.");
            }
            return View("Index", credentials);
        }

        public ActionResult Logout()
        {
            authService.Logout();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}