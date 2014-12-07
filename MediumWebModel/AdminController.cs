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
            var commandHandler = new AddPostCommandHandler();
            var command = new AddPostCommand
            {
                Title = post.Title,
                Body = post.Body,
                Published = post.Published
            };
            var postSlug = commandHandler.Handle(command);
            return RedirectToAction("Index", "Post", new {  postSlug });
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