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
        public ActionResult Login(CredentialsModel credentials)
        {
            var authenticator = new Authenticator();
            authenticator.Authenticate(credentials.Username, credentials.Password);
            if (authenticator.AuthenticationSuccessful)
            {
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "The username and password you entered did not match our records.");
            return View(credentials);
        }
    }
}