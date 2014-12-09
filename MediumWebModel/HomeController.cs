using System.Collections.Generic;
using System.Linq;
using MediumDomainModel;
using System.Web.Mvc;

namespace Medium.WebModel
{
    public class HomeController : Controller
    {
        private readonly IRequestHandler<AllPostsRequest, IEnumerable<PostModel>> allPostsRequestHandler;

        public HomeController(
            IRequestHandler<AllPostsRequest, IEnumerable<PostModel>> allPostsRequestHandler)
        {
            this.allPostsRequestHandler = allPostsRequestHandler;
        }

        public ActionResult Index()
        {
            var request = new AllPostsRequest();
            var model = allPostsRequestHandler.Handle(request)
                .Where(post => post.Published);
            return View(model);
        }
    }
}