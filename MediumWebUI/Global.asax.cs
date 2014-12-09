using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Medium.WebUI
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            ControllerBuilder.Current.SetControllerFactory(new CompositionRoot());
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{postSlug}",
                defaults: new { controller = "Home", action = "Index", postSlug = UrlParameter.Optional });
        }
    }
}