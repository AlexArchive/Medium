using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GoBlog.Domain.Infrastructure.Persistence.Migrations;
using GoBlog.Infrastructure.AutoMapper;

namespace GoBlog
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