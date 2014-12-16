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
            RouteTable.Routes.MapRoute(
                name: "Pagination",
                url: "{controller}/{action}/{pageNumber}",
                defaults: new { controller = "Home", action = "Index", pageNumber = UrlParameter.Optional });

            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{postSlug}",
                defaults: new { controller = "Home", action = "Index", postSlug = UrlParameter.Optional });
        }
    }
}