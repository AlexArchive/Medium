using System.Web.Mvc;

namespace GoBlog.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get { return "Admin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Login",
                url: "login/{action}",
                defaults: new { controller = "Login", action = "Index" }
            );

            context.MapRoute(
                name: "Admin_default",
                url: "admin/{controller}/{action}/{slug}",
                defaults: new { controller = "Posts", action = "Index", slug = UrlParameter.Optional },
                namespaces: new[] { "GoBlog.Areas.Admin.Controllers" }
            );
        }
    }
}