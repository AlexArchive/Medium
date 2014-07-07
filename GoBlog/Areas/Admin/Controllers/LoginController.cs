using System.Web.Mvc;
using System.Web.Routing;
using GoBlog.Areas.Admin.Models;
using GoBlog.Authentication;

namespace GoBlog.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController()
            : this(new AuthenticationService())
        {
        }

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public ActionResult Index()
        {

            if (authenticationService.Authenticated)
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
                var authenticated = authenticationService.Authenticate(credentials.Username, credentials.Password);
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
            authenticationService.Logout();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}