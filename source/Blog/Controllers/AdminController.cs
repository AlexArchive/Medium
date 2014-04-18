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
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.Username, model.Password))
                {
                    return Redirect(Request.QueryString["ReturnUrl"]);
                }

                ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            }
            return View(model);
        }
    }
}