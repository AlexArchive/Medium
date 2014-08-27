using GoBlog.Domain;
using System.Web.Mvc;

namespace GoBlog.Areas.Admin.Controllers 
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IPostsRepository repository;

        public AdminController(IPostsRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            var posts = repository.All();
            return View(posts);
        }
    }
}