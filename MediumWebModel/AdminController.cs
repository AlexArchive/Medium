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
            ICommandHandler<PostModel> commandHandler = new NewPostCommandHandler();
            commandHandler.Handle(post);
            return RedirectToAction("Index", "Post", new { postSlug = post.Slug });
        }

        public ActionResult EditPost(string postSlug)
        {
            var requestHandler = new PostDetailsRequestHandler();
            var model = requestHandler.Handle(postSlug);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPost(PostModel post)
        {
            ICommandHandler<PostModel> commandHandler = new EditPostCommandHandler();
            commandHandler.Handle(post);

            return RedirectToAction("Index", "Post", new { postSlug = post.Slug });
        }

        public ActionResult DeletePost(string postSlug)
        {
            DeleteCommand command = new DeleteCommand { PostSlug = postSlug };
            ICommandHandler<DeleteCommand> commandHandler = new DeleteCommandHandler();
            commandHandler.Handle(command);

            return RedirectToAction("Index");
        }
    }
}