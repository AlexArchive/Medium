using System.Web.Mvc;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class PostController : Controller
    {
        public ViewResult Index(string postSlug)
        {
            var handler = new PostDetailsRequestHandler();
            var model = handler.Handle(postSlug);
            return View(model);
        }
    }
}