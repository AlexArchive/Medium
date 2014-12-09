using System;
using System.Web.Mvc;
using System.Web.Routing;
using Medium.WebModel;
using MediumDomainModel;

namespace Medium.WebUI
{
    public class CompositionRoot : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(
            RequestContext requestContext,
            Type controllerType)
        {
            if (controllerType == typeof(HomeController))
            {
                return new HomeController(new AllPostsRequestHandler());
            }
            
            if (controllerType == typeof(PostController))
            {
                return new PostController(
                    new PostRequestHandler());
            }

            if (controllerType == typeof(AccountController))
            {
                return new AccountController(new Authenticator());
            }

            if (controllerType == typeof(AdminController))
            {
                return new AdminController(
                    new AllPostsRequestHandler(),
                    new PostRequestHandler(),
                    new AddPostCommandHandler(),
                    new EditPostCommandHandler(),
                    new DeletePostCommandHandler());
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}