using Blog.Areas.Admin.Models;
using Blog.Core.Security;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationProvider _authProvider;

        public LoginController()
        {
            _authProvider = new AuthenticationProvider();
        }
       
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectFromLoginPage();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var authenticated = _authProvider.Authenticate(model.Username, model.Password);
                if (authenticated)
                {
                    return RedirectFromLoginPage(Request.QueryString["ReturnUrl"]);
                }
                ModelState.AddModelError("", "Username or Password is incorrect.");
            }
            return View(model);
        }

        private ActionResult RedirectFromLoginPage(string returnUrl = "/admin")
        {
            return Redirect(returnUrl);
        }
    }
}