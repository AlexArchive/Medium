using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MediatR;
using Medium.DomainModel;
using Medium.WebModel;

namespace Medium.WebUI.CompositionRoot
{
    public class WindsorInstaller
    {
        public IWindsorContainer Install()
        {
            var container = new WindsorContainer();

            container.Register(Component
                .For<IDbConnection>()
                .UsingFactoryMethod(SqlConnectionFactory.Create)
                .LifestyleTransient());

            container.Register(Classes
                .FromAssemblyContaining<HomeController>()
                .BasedOn<IController>()
                .LifestylePerWebRequest());

            container.Register(Component
                .For<IMediator>()
                .ImplementedBy<Mediator>());

            container.Register(Component
                .For<SingleInstanceFactory>()
                .UsingFactoryMethod<SingleInstanceFactory>(k => t => k.Resolve(t)));

            container.Register(Component
                .For<MultiInstanceFactory>()
                .UsingFactoryMethod<MultiInstanceFactory>(k => t => (IEnumerable<object>) k.ResolveAll(t)));

            container.Register(Classes
                .FromAssemblyContaining<AddPostCommand>()
                .Pick()
                .WithServiceAllInterfaces());

            return container;
        }
    }
}