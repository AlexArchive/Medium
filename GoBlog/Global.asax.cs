using Autofac;
using Autofac.Integration.Mvc;
using GoBlog.Areas.Admin;
using GoBlog.Authentication;
using GoBlog.Domain;
using System.Data.Entity;
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
            builder.RegisterType<DatabaseContext>().AsSelf();
            builder.RegisterType<PostsRepository>().As<IPostsRepository>();
            builder.RegisterType<Authenticator>().As<IAuthenticator>();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(CreateContainer()));
            Database.SetInitializer(new DatabaseSeeder());
        }
    }
}
