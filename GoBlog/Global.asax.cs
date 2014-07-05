using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GoBlog.Infrastructure.AutoMapper;
using GoBlog.Persistence.Migrations;

namespace GoBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.Configure();
            Database.SetInitializer(new DatabaseSeeder());
        }
    }
}
