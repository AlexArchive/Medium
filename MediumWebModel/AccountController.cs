using System.Web.Mvc;

namespace Medium.WebModel
{
    public class AccountController : Controller
    {
        private readonly Authenticator authenticator = new Authenticator();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CredentialsModel credentials)
        {
            authenticator.Authenticate(
                credentials.Username, 
                credentials.Password,
                credentials.RememberMe);

            if (authenticator.AuthenticationSuccessful)
            {
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "The username and password you entered did not match our records.");
            
            return View(credentials);
        }

        public ActionResult Logout()
        {
            authenticator.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}