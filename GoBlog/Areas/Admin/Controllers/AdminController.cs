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

        public ViewResult Index()
        {
            var posts = repository.All();
            return View(posts);
        }

        public ActionResult Delete(string slug)
        {
            var success = repository.Delete(slug);

            if (success)
                TempData["Message"] = "Post deleted successfully.";
            else
                TempData["Message"] = "Failed to delete this post as it no longer exists.";

            return RedirectToAction("Index");
        }
    }
}