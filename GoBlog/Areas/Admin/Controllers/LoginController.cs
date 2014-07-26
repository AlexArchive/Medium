using GoBlog.Areas.Admin.Models;
using GoBlog.Authentication;
using System.Web.Mvc;

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

            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(CredentialsModel credentials)
        {
            if (!ModelState.IsValid) return View("Index", credentials);

            authService.Authenticate(credentials.Username, credentials.Password);

            if (authService.Authenticated) 
            {
                return Redirect(Request.QueryString["ReturnUrl"] ?? "/admin");
            }

            ModelState.AddModelError("", "Username or Password is incorrect.");
            return View(credentials);
        }

        public ActionResult Logout()
        {
            authService.Logout();
            return RedirectToAction("Index", "Posts", new { area = "" });
        }
    }
}