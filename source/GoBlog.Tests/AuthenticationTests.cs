using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
using GoBlog.Domain.Security;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoBlog.Tests
{
    [TestFixture]
    public class AuthenticationTests
    {
        private LoginController _loginController;
        private IAuthenticationProvider _authenticationProvider;
        private HttpContextBase _httpContext;

        [SetUp]
        public void SetUp()
        {
            _authenticationProvider = MockRepository.GenerateMock<IAuthenticationProvider>();
            _loginController = new LoginController(_authenticationProvider);

            var request = MockRepository.GenerateMock<HttpRequestBase>();
            var context = MockRepository.GenerateMock<HttpContextBase>();
            request.Stub(x => x.QueryString).Return(new NameValueCollection());
            context.Stub(x => x.Request).Return(request);

            _httpContext = context;
            _loginController.ControllerContext =
                new ControllerContext(_httpContext, new RouteData(), _loginController);
        }

        [Test]
        public void AuthenticatedUserDoesNotNeedToAuthenticate()
        {
            // arrange
            _authenticationProvider.Stub(s => s.Authenticate(null, null)).Return(true);

            // act
            var actual = _loginController.Index(new LoginModel());

            // assert
            Assert.IsInstanceOf<RedirectResult>(actual);
            Assert.AreEqual("/admin", ((RedirectResult)actual).Url);
        }

        [Test]
        public void NonAuthenticatedUserIsPresentedWithLogin()
        {
            // arrange
            _authenticationProvider.Stub(s => s.Authenticate(null, null)).Return(false);

            // act
            var actual = _loginController.Index(new LoginModel());

            // assert
            Assert.IsInstanceOf<ViewResult>(actual);
        }
    }
}