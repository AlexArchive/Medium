using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace Medium.WebModel
{
    public class RssActionResult : ActionResult
    {
        public SyndicationFeed Feed { get; private set; }

        public RssActionResult(SyndicationFeed syndication)
        {
            Feed = syndication;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";
            var formatter = new Rss20FeedFormatter(Feed);
            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formatter.WriteTo(writer);
            }
        }
    }
}