using System.Web.Mvc;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class PostController : Controller
    {
        public ActionResult Index(string postSlug)
        {
            var requestHandler = new PostRequestHandler();
            var request = new PostRequest { Slug = postSlug };
            var model = requestHandler.Handle(request);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}