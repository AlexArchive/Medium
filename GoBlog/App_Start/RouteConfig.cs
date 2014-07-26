using System.Web.Mvc;
using System.Web.Routing;

namespace GoBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Page",
                url: "page/{pageNumber}",
                defaults: new { controller = "Posts", action = "Index" },
                namespaces: new[] { "GoBlog.Controllers" }
            );

            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/{slug}",
                defaults: new { controller = "Posts", action = "Index", slug = UrlParameter.Optional },
                namespaces: new[] { "GoBlog.Controllers" }
            );
        }
    }
}