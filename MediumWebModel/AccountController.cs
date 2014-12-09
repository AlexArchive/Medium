using System.Web.Mvc;

namespace Medium.WebModel
{
    public class AccountController : Controller
    {
        private readonly IAuthenticator authenticator;

        public AccountController(IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(CredentialsModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }
            authenticator.Authenticate(credentials.Username, credentials.Password);
            if (authenticator.AuthenticationSuccessful)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "The username and password you entered did not match our records.");
                return View(credentials);
            }
        }

        public ActionResult Logout()
        {
            authenticator.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}