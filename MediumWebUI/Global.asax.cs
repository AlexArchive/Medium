using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Medium.WebUI.CompositionRoot;

namespace Medium.WebUI
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{postSlug}",
                defaults: new { controller = "Home", action = "Index", postSlug = UrlParameter.Optional });

            IWindsorContainer container = new WinsdorInstaller().Install();
            ControllerBuilder.Current.SetControllerFactory(new WinsdorCompositionRoot(container));
        }
    }
}