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
                name: "Post",
                url: "{slug}",
                defaults: new { controller = "Home", action = "Post" }
            );

            routes.MapRoute(
                name: "Pagination",
                url: "page/{pageNumber}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
