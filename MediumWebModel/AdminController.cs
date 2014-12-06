using System;
using System.Diagnostics;
using MediumDomainModel;
using System.Web.Mvc;

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
        public ActionResult NewPost(PostInput post)
        {
            ICommandHandler<NewPostCommand, string> commandHandler = new NewPostCommandHandler();

            NewPostCommand command = new NewPostCommand
            {
                Title = post.Title,
                Body = post.Body,
                Published = post.Published
            };

            var slug = commandHandler.Handle(command);

            return RedirectToAction("Index", "Post", new { postSlug = slug });
        }

        public ActionResult EditPost(string postSlug)
        {
            var requestHandler = new PostDetailsRequestHandler();
            var postModel = requestHandler.Handle(postSlug);

            var postInput = new PostInput
            {
                Slug = postModel.Slug,
                Title = postModel.Title,
                Body = postModel.Body,
                Published = postModel.Published,
            };

            return View(postInput);
        }

        [HttpPost]
        public ActionResult EditPost(PostInput post)
        {
            ICommandHandler<EditPostCommand, string> commandHandler = new EditPostCommandHandler();

            EditPostCommand command = new EditPostCommand
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
            DeletePostCommand command = new DeletePostCommand { PostSlug = postSlug };
            ICommandHandler<DeletePostCommand> commandHandler = new DeletePostCommandHandler();
            commandHandler.Handle(command);

            return RedirectToAction("Index");
        }
    }
}