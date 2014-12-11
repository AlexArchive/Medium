using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter.Unofficial;
using MediatR;
using Medium.WebModel;
using MediumDomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Medium.WebUI.CompositionRoot
{
    public class WindsorInstaller
    {
        public IWindsorContainer Install()
        {
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

            return container;
        }
    }
}