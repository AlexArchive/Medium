using System.Linq;
using System.Web.Mvc;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    public class HomeController : ControllerBase
    {
        public HomeController(IMediator mediator) : base(mediator)
        {
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var request = new PostPageRequest
            {
                IncludeDrafts = false,
                PageNumber = pageNumber,
                PostsPerPage = 5
            };

            var page = base.Mediator.Send(request);

            if (page.Any() || pageNumber == 1 || page.TotalPageCount == 0)
            {
                return View(page);
            }
            else
            {
                return RedirectToAction("Index", new { pageNumber = page.TotalPageCount });
            }
        }
    }
}