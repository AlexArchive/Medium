using System.Web.Mvc;
using MediatR;
using MediumDomainModel;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IMediator messageBus;

        public AdminController(IMediator messageBus)
        {
            this.messageBus = messageBus;
        }

        public ActionResult Index()
        {
            var request = new AllPostsRequest(includeDrafts: true);
            var posts = messageBus.Send(request);
            return View(posts);
        }

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult AddPost(PostInput postInput)
        {
            var command = postInput.MapTo<AddPostCommand>();
            var slug = messageBus.Send(command);

            if (slug != null)
            {
                TempData["alertVerb"] = "published";
                return RedirectToAction("EditPost", new { postSlug = slug });
            }

            ModelState.AddModelError("", "A post with this title already exists.");
            return View(postInput);
        }

        public ActionResult EditPost(PostRequest request)
        {
            var post = messageBus.Send(request);

            if (post == null)
            {
                return HttpNotFound();
            }

            var model = post.MapTo<PostInput>();
            return View(model);
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult EditPost(PostInput postInput)
        {
            var command = postInput.MapTo<EditPostCommand>();
            command.OriginalSlug = postInput.Slug;

            var updatedSlug = messageBus.Send(command);
            if (updatedSlug != null)
            {
                TempData["alertVerb"] = "updated";
                return RedirectToAction("EditPost", new { postSlug = updatedSlug });
            }

            ModelState.AddModelError("", "A post with this title already exists.");
            return View(postInput);
        }

        public ActionResult DeletePost(DeletePostCommand command)
        {
            messageBus.Send(command);
            TempData["Message"] = "Your post has been deleted.";
            return RedirectToAction("Index");
        }
    }
}