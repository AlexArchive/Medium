using System.Web.Mvc;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class PostController : Controller
    {
        private readonly IRequestHandler<PostRequest, PostModel> postRequestHandler;

        public PostController(IRequestHandler<PostRequest, PostModel> postRequestHandler)
        {
            this.postRequestHandler = postRequestHandler;
        }

        public ActionResult Index(string postSlug)
        {
            var request = new PostRequest { Slug = postSlug };
            var model = postRequestHandler.Handle(request);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}