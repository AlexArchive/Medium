using System.Web.Mvc;
using MediatR;

namespace Medium.WebModel
{
    public class AccountController : ControllerBase
    {
        private readonly Authenticator authenticator = new Authenticator();

        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
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