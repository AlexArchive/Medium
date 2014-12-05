using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Medium.WebUI
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{postSlug}",
                defaults: new { controller = "Home", action = "Index", postSlug = UrlParameter.Optional }
            );
        }
    }
}