using MediumDomainModel;
using System.Web.Mvc;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var requestHandler = new AllPostsRequestHandler();
            var request = new AllPostsRequest();
            var model = requestHandler.Handle(request);
            return View(model);
        }

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(PostInput post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            var slug = post.Title.ToSlug();
            var requestHandler = new PostRequestHandler();
            var request = new PostRequest { Slug = slug };
            if (requestHandler.Handle(request) != null)
            {
                ModelState.AddModelError("", "A post with this title already exists.");
                return View(post);
            }

            var commandHandler = new AddPostCommandHandler();
            var command = new AddPostCommand
            {
                Title = post.Title,
                Body = post.Body,
                Published = post.Published
            };
            var postSlug = commandHandler.Handle(command);
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
        public ActionResult EditPost(PostInput post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            var slug = post.Title.ToSlug();
            var requestHandler = new PostRequestHandler();
            var request = new PostRequest { Slug = slug };
            if (slug != post.Slug && requestHandler.Handle(request) != null)
            {
                ModelState.AddModelError("", "A post with this title already exists.");
                return View(post);
            }

            var commandHandler = new EditPostCommandHandler();
            var command = new EditPostCommand
            {
                Slug = post.Slug,
                Title = post.Title,
                Body = post.Body,
                Published = post.Published
            };
            var updatedSlug = commandHandler.Handle(command);
            return RedirectToAction("Index", "Post", new { postSlug = updatedSlug });
        }

        public ActionResult DeletePost(string postSlug)
        {
            var commandHandler = new DeletePostCommandHandler();
            var command = new DeletePostCommand { Slug = postSlug };
            commandHandler.Handle(command);
            return RedirectToAction("Index");
        }
    }
}