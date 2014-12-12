using System;
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
            var request = new AllPostsRequest();
            var model = requestBus.Send(request);
            return View(model);
        }

        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult AddPost(PostInput postInput)
        {
            var addPostCommand = new AddPostCommand
            {
                Title = postInput.Title,
                Body = postInput.Body,
                Published = postInput.Published
            };

            var response = requestBus.Send(addPostCommand);

            if (response == null)
            {
                ModelState.AddModelError("", "A post with this title already exists.");
                return View(postInput);
            }

            return RedirectToAction("Index", "Post", new { postSlug = response });
        }

        public ActionResult EditPost(string postSlug)
        {
            var postRequest = new PostRequest {PostSlug = postSlug};
            var post = requestBus.Send(postRequest);
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
            var editPostCommand = new EditPostCommand
            {
                Slug = postInput.Slug,
                Title = postInput.Title,
                Body = postInput.Body,
                Published = postInput.Published
            };

            var response = requestBus.Send(editPostCommand);

            if (response == null)
            {
                ModelState.AddModelError("", "A post with this title already exists.");
                return View(postInput);
            }

            return RedirectToAction("Index", "Post", new { postSlug = response });

            //var postSlug = SlugConverter.Convert(postInput.Title);
            //var postRequest = new PostRequest {PostSlug = postSlug};
            //var post = requestBus.Send(postRequest);
            //if (postSlug != postInput.Slug && post != null)
            //{
            //    ModelState.AddModelError("", "A post with this title already exists.");
            //    return View(postInput);
            //}

            //var editPostCommand = new EditPostCommand
            //{
            //    Slug = postInput.Slug,
            //    Title = postInput.Title,
            //    Body = postInput.Body,
            //    Published = postInput.Published
            //};
            //var updatedSlug = requestBus.Send(editPostCommand);
            //return RedirectToAction("Index", "Post", new {postSlug = updatedSlug});
        }

        public ActionResult DeletePost(DeletePostCommand command)
        {
            requestBus.Send(command);
            return RedirectToAction("Index");
        }
    }
}