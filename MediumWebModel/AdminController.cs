using System.Web.Mvc;
using MediumDomainModel;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var handler = new PostRequestHandler();
            var model = handler.Handle();
            return View(model);
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
            return RedirectToAction("Index", "Post", new { postSlug = post.Slug });
        }

        public ActionResult EditPost(string postSlug)
        {
            var handler = new PostDetailsRequestHandler();
            var model = handler.Handle(postSlug);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPost(PostModel post)
        {
            var handler = new EditPostCommandHandler();
            handler.Handle(post);

            return RedirectToAction("Index", "Post", new { postSlug = post.Slug });
        }
    }
}