using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Medium.WebUI.CompositionRoot
{
    public class WinsdorCompositionRoot : DefaultControllerFactory
    {
         private readonly IWindsorContainer container;

         public WinsdorCompositionRoot(IWindsorContainer container)
         {
             this.container = container;
         }
         
         protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
         {
             return (IController)container.Resolve(controllerType);
         }
         
         public override void ReleaseController(IController controller)
         {
             container.Release(controller);
         }
    }
}