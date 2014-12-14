using System.Web.Mvc;
using MarkdownSharp;
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
            var model = bus.Send(request);
            if (model == null)
            {
                return HttpNotFound();
            }

            var options = new MarkdownOptions { AutoHyperlink = true };
            var markdown = new Markdown(options);

            model.Body = markdown.Transform(model.Body);
            
            return View(model);
        }
    }
}