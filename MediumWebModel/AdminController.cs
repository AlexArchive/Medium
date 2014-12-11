using MediumDomainModel;
using System.Web.Mvc;
using MediatR;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        private IMediator requestBroker;

        public AdminController(IMediator requestBroker)
        {
            this.requestBroker = requestBroker;
        }

        public ActionResult Index()
        {
            var request = new AllPostsRequest();
            var model = requestBroker.Send(request);
            return View(model);
        }

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult AddPost(PostInput post)
        {
            var slug = SlugConverter.Convert(post.Title);
            var request = new PostRequest { Slug = slug };
            if (requestBroker.Send(request) != null)
            {
                ModelState.AddModelError("", "A post with this title already exists.");
                return View(post);
            }

            var command = new AddPostCommand
            {
                Title = post.Title,
                Body = post.Body,
                Published = post.Published
            };
            var postSlug = requestBroker.Send(command);
            return RedirectToAction("Index", "Post", new { postSlug });
        }

        public ActionResult EditPost(string postSlug)
        {
            var requestHandler = new PostRequestHandler();
            var request = new PostRequest { Slug = postSlug };
            var model = requestHandler.Handle(request);
            var postInput = new PostInput
            {
                Slug = model.Slug,
                Title = model.Title,
                Body = model.Body,
                Published = model.Published,
            };
            return View(postInput);
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult EditPost(PostInput post)
        {
            var slug = SlugConverter.Convert(post.Title);
            var request = new PostRequest { Slug = slug };
            if (slug != post.Slug && requestBroker.Send(request) != null)
            {
                ModelState.AddModelError("", "A post with this title already exists.");
                return View(post);
            }

            var command = new EditPostCommand
            {
                Slug = post.Slug,
                Title = post.Title,
                Body = post.Body,
                Published = post.Published
            };
            var updatedSlug = requestBroker.Send(command);
            return RedirectToAction("Index", "Post", new { postSlug = updatedSlug });
        }

        public ActionResult DeletePost(string postSlug)
        {
            var command = new DeletePostCommand { Slug = postSlug };
            requestBroker.Send(command);
            return RedirectToAction("Index");
        }
    }
}