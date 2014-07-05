using System.Web.Mvc;
using GoBlog.Areas.Admin.Models;
using GoBlog.Authentication;

namespace GoBlog.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationHandler authHandler;

        public LoginController()
            : this (new AuthenticationHandler())
        {
        }

        public LoginController(IAuthenticationHandler authHandler)
        {
            this.authHandler = authHandler;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Authenticate(CredentialsModel credentials)
        {
            if (ModelState.IsValid)
            {
                var authenticated = authHandler.Authenticate(credentials.Username, credentials.Password);
                if (authenticated)
                {
                    return Redirect(Request.QueryString["ReturnUrl"] ?? "/admin");
                }

                ModelState.AddModelError("", "Username or Password is incorrect.");
            }
            return View("Index", credentials);
        }
    }
}