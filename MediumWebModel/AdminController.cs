using System.Web.Mvc;
using MediumDomainModel;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewPost(PostModel post)
        {
            var handler = new NewPostHandler();
            handler.Handle(post);
            return RedirectToAction("Index", "Post", new {postSlug = post.Slug});
        }
    }
}