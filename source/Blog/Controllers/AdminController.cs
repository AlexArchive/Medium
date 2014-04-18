using System.Web.Mvc;
using Blog.Core.Security;
using Blog.Models.AdminModel;

namespace Blog.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAuthenticationProvider _authProvider = new AuthenticationProvider();

        public ActionResult Index()
        {
            return View("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return HttpNotFound();
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var authenticated = _authProvider.Authenticate(model.Username, model.Password);
                if (authenticated)
                {
                    return Redirect(Request.QueryString["ReturnUrl"]);
                }

                ModelState.AddModelError(string.Empty, "Username or Password Incorrect");
            }

            return View(model);
        }
    }
}