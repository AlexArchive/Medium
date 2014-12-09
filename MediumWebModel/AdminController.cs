using System.Collections.Generic;
using MediumDomainModel;
using System.Web.Mvc;

namespace Medium.WebModel
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IRequestHandler<AllPostsRequest, IEnumerable<PostModel>> allPostsRequestHandler;
        private readonly IRequestHandler<PostRequest, PostModel> postRequestHandler;
        private readonly ICommandHandler<AddPostCommand, string> addPostCommandHandler;
        private readonly ICommandHandler<EditPostCommand, string> editPostCommandHandler;
        private readonly ICommandHandler<DeletePostCommand> deletePostCommandHandler;

        public AdminController(
            IRequestHandler<AllPostsRequest, IEnumerable<PostModel>> allPostsRequestHandler, 
            IRequestHandler<PostRequest, PostModel> postRequestHandler, 
            ICommandHandler<AddPostCommand, string> addPostCommandHandler, 
            ICommandHandler<EditPostCommand, string> editPostCommandHandler, 
            ICommandHandler<DeletePostCommand> deletePostCommandHandler)
        {
            this.allPostsRequestHandler = allPostsRequestHandler;
            this.postRequestHandler = postRequestHandler;
            this.addPostCommandHandler = addPostCommandHandler;
            this.editPostCommandHandler = editPostCommandHandler;
            this.deletePostCommandHandler = deletePostCommandHandler;
        }

        public ActionResult Index()
        {
            var request = new AllPostsRequest();
            var model = allPostsRequestHandler.Handle(request);
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
            var request = new PostRequest { Slug = slug };
            if (postRequestHandler.Handle(request) != null)
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
            var postSlug = addPostCommandHandler.Handle(command);
            return RedirectToAction("Index", "Post", new { postSlug });
        }

        public ActionResult EditPost(string postSlug)
        {
            var request = new PostRequest { Slug = postSlug };
            var model = postRequestHandler.Handle(request);
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
            var request = new PostRequest { Slug = slug };
            if (slug != post.Slug && postRequestHandler.Handle(request) != null)
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
            var updatedSlug = editPostCommandHandler.Handle(command);
            return RedirectToAction("Index", "Post", new { postSlug = updatedSlug });
        }

        public ActionResult DeletePost(string postSlug)
        {
            var command = new DeletePostCommand { Slug =postSlug };
            deletePostCommandHandler.Handle(command);
            return RedirectToAction("Index");
        }
    }
}