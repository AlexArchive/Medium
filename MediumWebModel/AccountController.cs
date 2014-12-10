using System.Web.Mvc;

namespace Medium.WebModel
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult Login(CredentialsModel credentials)
        {
            var authenticator = new Authenticator();
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
            var authenticator = new Authenticator();
            authenticator.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}