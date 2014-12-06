using System.Web.Mvc;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class PostController : Controller
    {
        public ActionResult Index(string postSlug)
        {
            var handler = new PostDetailsRequestHandler();
            var model = handler.Handle(postSlug);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}