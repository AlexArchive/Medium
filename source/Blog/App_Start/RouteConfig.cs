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
                name: "About",
                url: "about-me",
                defaults: new { controller = "Blog", action = "About" }
            );

            routes.MapRoute(
                name: "Tags",
                url: "tags",
                defaults: new { controller = "Blog", action = "Tags" }
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
