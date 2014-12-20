using System.Web.Mvc;
using MediatR;

namespace Medium.WebModel
{
    public class TagsController : Controller
    {
        private readonly IMediator requestBus;

        public TagsController(IMediator requestBus)
        {
            this.requestBus = requestBus;
        }


        public ActionResult Index()
        {
            return View();
        }
    }
}