using Moq;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoBlog.Test.Support
{
    namespace UnitTests
    {
        public static class ContextMockHelper
        {
            public static void SetFakeControllerContext(this Controller controller, NameValueCollection querystring = null)
            {
                var context = new Mock<HttpContextBase>();
                context.Setup(c => c.Request.QueryString).Returns(querystring ?? new NameValueCollection());
                var controllerBase = new Mock<ControllerBase>();
                var routeData = new RouteData();
                controller.ControllerContext = new ControllerContext(context.Object, routeData, controllerBase.Object);
            }
        }
    }
}