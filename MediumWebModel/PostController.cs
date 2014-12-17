using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    public class PostController : Controller
    {
        private readonly IMediator bus;

        public PostController(IMediator bus)
        {
            this.bus = bus;
        }

        public ActionResult Index(PostRequest request)
        {
            var postModel = bus.Send(request);
            if (postModel == null || postModel.Published == false)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }
    }
}