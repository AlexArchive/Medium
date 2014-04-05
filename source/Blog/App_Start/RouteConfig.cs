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
                name: "BlogPage",
                url: "{controller}/{pageNumber}",
                defaults: new { controller = "Blog", action = "Index", pageNumber = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{pageNumber}",
                defaults: new { controller = "Blog", action = "Index", pageNumber = UrlParameter.Optional }
            );
        }
    }
}
