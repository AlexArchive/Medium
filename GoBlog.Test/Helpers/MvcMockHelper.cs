using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace GoBlog.Test.Helpers
{
    namespace UnitTests
    {
        public static class MvcMockHelper
        {
            public static void SetFakeControllerContext(this Controller controller, NameValueCollection querystring = null)
            {
                var context = new Mock<HttpContextBase>();
                context.Setup(c => c.Request.QueryString).Returns(querystring ?? new NameValueCollection());
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(),
                    new Mock<ControllerBase>().Object);
            }
        }
    }
}