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
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult AddPost(PostInput postInput)
        {
            var command = new AddPostCommand
            {
                Title = postInput.Title,
                Body = postInput.Body,
                Published = postInput.Published
            };
            var postSlug = requestBus.Send(command);
            if (postSlug != null)
            {
                return RedirectToAction("Index", "Post", new { postSlug });
            }
            ModelState.AddModelError("", "A post with this title already exists.");
            return View(postInput);
        }

        public ActionResult EditPost(PostRequest request)
        {
            var post = requestBus.Send(request);
            var postInput = new PostInput
            {
                Slug = post.Slug,
                Title = post.Title,
                Body = post.Body,
                Published = post.Published
            };
            return View(postInput);
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult EditPost(PostInput postInput)
        {
            var command = new EditPostCommand
            {
                OriginalSlug = postInput.Slug,
                Title = postInput.Title,
                Body = postInput.Body,
                Published = postInput.Published
            };
            var updatedSlug = requestBus.Send(command);
            if (updatedSlug != null)
            {
                return RedirectToAction("Index", "Post", new { postSlug = updatedSlug });
            }
            ModelState.AddModelError("", "A post with this title already exists.");
            return View(postInput);
        }
    }
}