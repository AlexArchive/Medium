using System;
using System.Web.Mvc;
using System.Web.Routing;
using Medium.WebModel;

namespace Medium.WebUI
{
    public class CompositionRoot : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(AccountController))
            {
                IAuthenticator authenticator = new Authenticator();
                return new AccountController(authenticator);
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}