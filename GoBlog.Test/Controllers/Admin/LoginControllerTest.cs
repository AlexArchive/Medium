using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
using GoBlog.Authentication;
using GoBlog.Test.Helpers.UnitTests;
using Moq;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace GoBlog.Test.Controllers
{
    [TestFixture]
    public class LoginControllerTest
    {
        private CredentialsModel credentials;
        private Mock<IAuthenticationService> authHandler;
        private LoginController loginController;

        [SetUp]
        public void SetUp()
        {
            authHandler = new Mock<IAuthenticationService>();
            loginController = new LoginController(authHandler.Object);
            loginController.SetFakeControllerContext();
            credentials = new CredentialsModel { Username = "admin", Password = "password" };
        }

        [Test]
        public void Index()
        {
            var actual = loginController.Index() as ViewResult;

            Assert.NotNull(actual);
            Assert.AreEqual("Index", actual.ViewName);
        }

        [Test]
        public void IndexWhenAuthenticated()
        {
            authHandler.Setup(x => x.Authenticated).Returns(true);

            var actual = loginController.Index() as RedirectResult;

            Assert.NotNull(actual);
            Assert.That(actual.Url, Is.EqualTo("/admin"));
        }

        [Test]
        public void Authenticate()
        {
            authHandler.Setup(handler => handler.Authenticate(credentials.Username, credentials.Password))
                       .Returns(true);

            var actual = loginController.Authenticate(credentials) as RedirectResult;

            Assert.NotNull(actual);
            Assert.That(actual.Url, Is.EqualTo("/admin"));
            // Thought: Should I assert that the IAuthenticationHandler.Authenticated property is set to true?
        }

        [Test]
        public void AuthenticateWithReturnUrl()
        {
            var queryParams = new NameValueCollection { { "ReturnUrl", "/admin/settings" } };
            authHandler.Setup(handler => handler.Authenticate("admin", "password")).Returns(true);
            loginController.SetFakeControllerContext(queryParams);

            var actual = loginController.Authenticate(credentials) as RedirectResult;

            Assert.NotNull(actual);
            Assert.That(actual.Url, Is.EqualTo("/admin/settings"));
        }

        [Test]
        public void InvalidCredentials()
        {
            authHandler.Setup(handler => handler.Authenticate("admin", "password")).Returns(false);

            var actual = loginController.Authenticate(credentials) as ViewResult;

            Assert.NotNull(actual);
            Assert.That(loginController.ModelState[""].Errors[0].ErrorMessage == "Username or Password is incorrect.");
        }

        [Test]
        public void Logout()
        {
            var actual = loginController.Logout() as RedirectToRouteResult;
            
            Assert.NotNull(actual);
            Assert.That(actual.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(actual.RouteValues["controller"], Is.EqualTo("Home"));
        }
    }
}