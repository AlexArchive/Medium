using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter.Unofficial;
using MediatR;
using Medium.WebModel;
using Medium.WebUI.CompositionRoot;
using MediumDomainModel;
using Microsoft.Practices.ServiceLocation;

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
            


            var container = new WindsorContainer();
            
            container.Register(Classes
                .FromAssemblyContaining<HomeController>()
                .BasedOn<IController>()
                .LifestylePerWebRequest());

            container.Register(Classes
                .FromAssemblyContaining<IMediator>()
                .Pick()
                .WithServiceAllInterfaces());

            container.Register(Classes
                .FromAssemblyContaining<AddPostCommand>()
                .Pick()
                .WithServiceAllInterfaces());

            container.Kernel.AddHandlersFilter(new ContravariantFilter());

            var serviceLocator = new WindsorServiceLocator(container);
            var serviceLocatorProvider = new ServiceLocatorProvider(() => serviceLocator);
            container.Register(Component
                .For<ServiceLocatorProvider>()
                .Instance(serviceLocatorProvider));

            ControllerBuilder.Current.SetControllerFactory(new WinsdorCompositionRoot(container));
        }
    }
}