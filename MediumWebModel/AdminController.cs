using System.Web.Mvc;
using MediatR;
using MediumDomainModel;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IMediator requestBus;

        public AdminController(IMediator requestBus)
        {
            this.requestBus = requestBus;
        }

        public ActionResult Index()
        {
            var request = new AllPostsRequest
            {
                IncludeDrafts = true
            };
            var posts = requestBus.Send(request);
            return View(posts);
        }

        public ActionResult AddPost()
        {
            return View();
        }

        public ActionResult DeletePost(DeletePostCommand command)
        {
            requestBus.Send(command);
            TempData["Message"] = "Your post has been deleted.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult AddPost(PostInput postInput)
        {
            var command = postInput.MapTo<AddPostCommand>();
            var postSlug = requestBus.Send(command);
            if (postSlug != null)
            {
                var helper = new UrlHelper(ControllerContext.RequestContext);
                TempData["PostUrl"] = helper.Action("Index", "Post", new { postSlug });
                return RedirectToAction("EditPost", new { postSlug });
            }
            ModelState.AddModelError("", "A post with this title already exists.");
            return View(postInput);
        }

        public ActionResult EditPost(PostRequest request)
        {
            var post = requestBus.Send(request);
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
            var updatedSlug = requestBus.Send(command);
            if (updatedSlug != null)
            {
                var helper = new UrlHelper(ControllerContext.RequestContext);
                ViewBag.PostUrl = helper.Action("Index", "Post", new { postSlug = updatedSlug });
                return View(postInput);
            }
            ModelState.AddModelError("", "A post with this title already exists.");
            return View(postInput);
        }
    }
}