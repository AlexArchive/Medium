using System.Data.Entity;
using Blog.Core.Infrastructure.Persistence.Migrations;
using Blog.Infrastructure.AutoMapper;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfiguration.Configure();
            Database.SetInitializer(new Configuration());
        }
    }
}