using Autofac;
using Autofac.Integration.Mvc;
using GoBlog.Authentication;
using GoBlog.Infrastructure.AutoMapper;
using GoBlog.Persistence;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoBlog
{
    public class MvcApplication : HttpApplication
    {
        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterTypes(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            return builder.Build();
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<BlogDatabase>().As<IRepository>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
        }

        protected void Application_Start()
        {
            AutoMapperConfig.Configure();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(CreateContainer()));
        }
    }
}
