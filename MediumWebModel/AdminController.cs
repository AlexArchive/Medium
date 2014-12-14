using System.Web.Mvc;
using MediatR;
using MediumDomainModel;

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

        public ActionResult Index()
        {
            var request = new AllPostsRequest { IncludeDrafts = true };
            var posts = bus.Send(request);
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