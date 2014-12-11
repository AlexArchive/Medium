using System.Web.Mvc;
using MediatR;
using MediumDomainModel;

namespace Medium.WebModel
{
    public class PostController : Controller
    {
        private readonly IMediator requestBroker;

        public PostController(IMediator requestBroker)
        {
            this.requestBroker = requestBroker;
        }

        public ActionResult Index(string postSlug)
        {
            var request = new PostRequest { Slug = postSlug };
            var model = requestBroker.Send(request);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}