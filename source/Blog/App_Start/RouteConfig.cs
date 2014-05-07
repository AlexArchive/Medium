using System.Web.Mvc;
using System.Web.Routing;

namespace Blog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Not Found Error",
                url: "not-found",
                defaults: new { controller = "Error", action = "NotFound" }
            );

            routes.MapRoute(
                name: "Server Error",
                url: "server-error",
                defaults: new { controller = "Error", action = "ServerError" }
            );

            routes.MapRoute(
                name: "About",
                url: "about-me",
                defaults: new { controller = "Blog", action = "About" }
            );

            routes.MapRoute(
                name: "Archive",
                url: "archive",
                defaults: new { controller = "Blog", action = "Archive" }
            );

            routes.MapRoute(
                name: "Entry",
                url: "{headerSlug}",
                defaults: new { controller = "Blog", action = "Entry" }
            );

            routes.MapRoute(
                name: "Page",
                url: "page/{pageNumber}",
                defaults: new { controller = "Blog", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
