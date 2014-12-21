using System.Linq;
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
            var request = new PostPageRequest
            {
                IncludeDrafts = true,
                PageNumber = pageNumber,
                PostsPerPage = 20
            };

            var page = bus.Send(request);

            if (page.Any() || pageNumber == 1 || page.TotalPageCount == 0)
            {
                return View(page);
            }
            else
            {
                return RedirectToAction("Index", new { pageNumber = page.TotalPageCount });
            }
        }

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
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

            var postInput = new PostInput
            {
                Slug = post.Slug,
                Title = post.Title,
                Body = post.Body,
                Published = post.Published,
                Tags = TagConverter.Convert(post.Tags.Select(tag => tag.Name))
            };
            return View(postInput);
        }

        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
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
            var deleted = bus.Send(command);

            if (deleted)
            {
                TempData["Criticalness"] = AlertCriticalness.Success;
                TempData["Message"] = "Your post has been deleted.";
            }
            else
            {
                TempData["Criticalness"] = AlertCriticalness.Danger;
                TempData["Message"] = "The post you tried to delete does not exist.";
            }

            return RedirectToAction("Index");
        }
    }
}