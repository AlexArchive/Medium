using MediatR;
using Medium.DomainModel;
using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace Medium.WebModel
{
    public class RssController : ControllerBase
    {
        public RssController(IMediator mediator)
            : base(mediator)
        {
        }

        public ActionResult Index()
        {
            var posts =
                base.Mediator.Send(new PostPageRequest { IncludeDrafts = false, PageNumber = 1, PostsPerPage = 15 });

            var syndicationItems = posts.Select(post => new SyndicationItem(
                post.Title,
                post.Body,
                new Uri(Url.Action("Index", "Post", new { postSlug = post.Slug }, Request.Url.Scheme)),
                post.Slug,
                post.PublishedAt));

            var syndicationFeed = new SyndicationFeed(
                base.Configuration.BlogTitle,
                "Medium is a beautiful blog engine",
                new Uri(Url.Action("Index", "Home", null, Request.Url.Scheme)),
                syndicationItems);

            return new RssActionResult(syndicationFeed);
        }
    }
}