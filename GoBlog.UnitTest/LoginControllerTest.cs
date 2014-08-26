using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
using GoBlog.Authentication;
using GoBlog.Controllers;
using GoBlog.UnitTest.Support;
using Moq;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace GoBlog.UnitTest
{
    [TestFixture]
    public class LoginControllerTest
    {
        private LoginController controller;
        private Mock<IAuthenticator> authenticator;

        [SetUp]
        public void SetUp()
        {
            authenticator = new Mock<IAuthenticator>();
            controller = new LoginController(authenticator.Object);
            controller.SetupControllerContext();
       }

        [Test]
        public void Index_RendersDefaultView()
        {
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Index_AlreadyAuthenticatd_RedirectsToAdmin()
        {
            authenticator.Setup(auth => auth.Authenticated).Returns(true);

            controller
                .WithCallTo(c => c.Index())
                .ShouldRedirectTo<AdminController>(c => c.Index());
        }

        [Test]
        public void Login_AuthenticationFailure_RendersCorrectView()
        {
            var credentials = new CredentialsModel();

            controller
                .WithCallTo(c => c.Login(credentials))
                .ShouldRenderView("Index");
        }

        [Test]
        public void Login_AuthenticationFailure_ReturnsError()
        {
            var credentials = new CredentialsModel();

            controller
                .WithCallTo(c => c.Login(credentials))
                .ShouldRenderView("Index")
                .WithModel(credentials)
                .AndModelError("").ThatEquals("Invalid username or password.");
        }

        [Test]
        public void Login_AuthenticationSuccess_RedirectsToAdmin()
        {
            var credentials = new CredentialsModel();
            authenticator.Setup(auth => auth.Authenticated).Returns(true);

            controller
                .WithCallTo(c => c.Login(credentials))
                .ShouldRedirectTo<AdminController>(c => c.Index());
            authenticator.Verify(auth => auth.Authenticate(null, null), Times.Once);
        }

        [Test]
        public void Login_AuthenticationSuccess_RedirectsToReturnUrl()
        {
            const string ReturnUrl = "admin/abc";
            var credentials = new CredentialsModel();
            authenticator.Setup(auth => auth.Authenticated).Returns(true);
            controller.SetupControllerContext(new NameValueCollection {{"ReturnUrl", ReturnUrl}});

            controller
                .WithCallTo(c => c.Login(credentials))
                .ShouldRedirectTo(ReturnUrl);
            authenticator.Verify(auth => auth.Authenticate(null, null), Times.Once);
        }

        [Test]
        public void Logout_RedirectsToHome()
        {
            controller
                .WithCallTo(c => c.Logout())
                .ShouldRedirectTo<HomeController>(c => c.Index(1));
            authenticator.Verify(c => c.Logout(), Times.Once);
        }

        [Test, Ignore]
        public void Logout_NotAuthenticated_DoesWhat()
        {
            
        }

        [Test]
        public void Controller_HasAuthorizeAttribute()
        {
            var attributes = typeof(AdminController)
                .GetCustomAttributes(typeof (AuthorizeAttribute), true);
            
            Assert.IsNotEmpty(attributes);
        }
    }
}