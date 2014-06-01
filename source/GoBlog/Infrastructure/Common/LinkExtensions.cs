using System.Web.Mvc;
using System.Web.Mvc.Html;
using GoBlog.Models;

namespace GoBlog.Infrastructure.Common
{
    public static class LinkExtensions
    {
        public static MvcHtmlString LinkToEntry(this HtmlHelper helper, EntryViewModel entry, object htmlAttributes = null)
        {
            return helper.ActionLink(entry.Header, "Entry", "Blog", new { entry.Slug }, htmlAttributes);
        }

        public static string LinkToEntry(this UrlHelper helper, string slug)
        {
            return helper.RouteUrl(new {area = "", controller = "Blog", action = "Entry", slug});
        }
    }
}