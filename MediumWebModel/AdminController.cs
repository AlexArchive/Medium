using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IMediator bus;

        public AdminController(IMediator bus)
        {
            this.bus = bus;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var request = new PostPageRequest(
                pageNumber,
                postsPerPage: 10,
                includeDrafts: true);

            var page = bus.Send(request);
            return View(page);
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
            var commandResponse = bus.Send(command);

            if (commandResponse != null)
            {
                TempData["alertVerb"] = "published";

                return RedirectToAction("EditPost",
                    new { postSlug = commandResponse });
            }

            ModelState.AddModelError("", "A post with this title already exists.");
            return View(postInput);
        }

        public ActionResult EditPost(PostRequest request)
        {
            var post = bus.Send(request);

            if (post == null)
            {
                return HttpNotFound();
            }

            var postInput = post.MapTo<PostInput>();
            return View(postInput);
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult EditPost(PostInput postInput)
        {
            var command = postInput.MapTo<EditPostCommand>();
            command.OriginalSlug = postInput.Slug;

            var updatedSlug = bus.Send(command);
            if (updatedSlug != null)
            {
                TempData["alertVerb"] = "updated";

                return RedirectToAction("EditPost",
                    new { postSlug = updatedSlug });
            }

            ModelState.AddModelError("", "A post with this title already exists.");
            return View(postInput);
        }

        public ActionResult DeletePost(DeletePostCommand command)
        {
            bus.Send(command);
            TempData["Message"] = "Your post has been deleted.";
            return RedirectToAction("Index");
        }
    }
}