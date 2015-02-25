using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Medium.WebUI.CompositionRoot;

namespace Medium.WebUI
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            InstallWindsor();
            ConfigureRoutes();
        }

        private void InstallWindsor()
        {
            ControllerBuilder.Current
                .SetControllerFactory(new WindsorCompositionRoot(new WindsorInstaller().Install()));
        }

        private void ConfigureRoutes()
        {
            RouteTable.Routes.LowercaseUrls = true;

            RouteTable.Routes.IgnoreRoute(
                url: "{resource}.axd/{*pathInfo}");

            RouteTable.Routes.MapRoute(
                name: "Archive",
                url: "archive",
                defaults: new { controller = "Archive", action = "Index" });

            RouteTable.Routes.MapRoute(
                name: "Tags",
                url: "tags",
                defaults: new { controller = "Tags", action = "Index" });

            RouteTable.Routes.MapRoute(
                name: "Admin",
                url: "admin",
                defaults: new { controller = "Admin", action = "Index" });

            RouteTable.Routes.MapRoute(
                name: "Page",
                url: "page/{pageNumber}",
                defaults: new { controller = "Home", action = "Index" }
            );

            RouteTable.Routes.MapRoute(
                name: "Post",
                url: "{postSlug}",
                defaults: new { controller = "Post", action = "Index" });

            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{postSlug}",
                defaults: new { controller = "Home", action = "Index", postSlug = UrlParameter.Optional });
        }
    }
}