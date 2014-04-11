using System.Web.Mvc;
using System.Web.Mvc.Html;
using Blog.Core.Data.Entities;

namespace Blog.Helpers
{
    public static class ActionLinkHelpers
    {
        public static MvcHtmlString EntryLink(this HtmlHelper helper, BlogEntry entry)
        {
            return helper.ActionLink(
                linkText: entry.Header,
                actionName: "Entry",
                controllerName: "Blog",
                routeValues: new { headerSlug = entry.HeaderSlug }, 
                htmlAttributes: null
            );
        }
    }
}