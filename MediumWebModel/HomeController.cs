using System.Web.Mvc;
using MarkdownSharp;
using MediatR;
using Medium.DomainModel;

namespace Medium.WebModel
{
    public class HomeController : Controller
    {
        private readonly IMediator bus;

        public HomeController(IMediator bus)
        {
            this.bus = bus;
        }

        public ActionResult Index()
        {
            var request = new AllPostsRequest { IncludeDrafts = false };
            var posts = bus.Send(request);
            
            var options = new MarkdownOptions { AutoHyperlink = true };
            var markdown = new Markdown(options);
            foreach (var post in posts)
            {
                post.Body = markdown.Transform(post.Body);
            }

            return View(posts);
        }
    }
}