using System.Web.Mvc;

namespace GoBlog.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Login",
                "login/{action}",
                new { controller = "Login", action = "Index" }
            );

            context.MapRoute(
                "Admin_default",
                "admin",
                new { controller="Home", action = "Index" },
                namespaces: new[] { "GoBlog.Areas.Admin.Controllers" }
            );
        }
    }
}