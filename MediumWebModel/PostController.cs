using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    public class PostController : ControllerBase
    {
        public PostController(IMediator mediator):base(mediator)
        {
        }

        public ActionResult Index(PostRequest request)
        {
            var postModel = base.Mediator.Send(request);
            if (postModel == null || (postModel.Published == false && !Request.IsAuthenticated))
            {
                return HttpNotFound();
            }
            return View(postModel);
        }
    }
}