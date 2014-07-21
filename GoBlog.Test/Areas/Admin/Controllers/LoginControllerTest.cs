using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
using GoBlog.Authentication;
using GoBlog.Test.Support.UnitTests;
using Moq;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace GoBlog.Test.Areas.Admin.Controllers
{
    [TestFixture]
    public class LoginControllerTest
    {
        private CredentialsModel credentials;
        private Mock<IAuthenticationService> authService;
        private LoginController loginController;

        [SetUp]
        public void SetUp()
        {
            authService = new Mock<IAuthenticationService>();
            loginController = new LoginController(authService.Object);
            loginController.SetFakeControllerContext();
            credentials = new CredentialsModel { Username = "admin", Password = "password" };
        }

        [Test]
        public void Index_ReturnsCorrectView()
        {
            var actual = loginController.Index() as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Index_AlreadyAuthenticated_RedirectsToAdmin()
        {
            authService.Setup(service => service.Authenticated).Returns(true);

            var actual = loginController.Index() as RedirectResult;

            Assert.NotNull(actual);
            Assert.That(actual.Url, Is.EqualTo("/admin"));
        }

        [Test]
        public void Authenticate_ValidCredentials_RedirectsToAdmin()
        {
            authService.Setup(service => service.Authenticate(credentials.Username, credentials.Password))
                       .Returns(true);

            var actual = loginController.Authenticate(credentials) as RedirectResult;

            Assert.NotNull(actual);
            Assert.That(actual.Url, Is.EqualTo("/admin"));
            // Thought: Assert IAuthenticationService.Authenticated == true?
        }

        [Test]
        public void Authenticate_ValidCredentials_WithReturnUrl_RedirectsToReturnUrl()
        {
            authService.Setup(service => service.Authenticate(credentials.Username, credentials.Password))
                       .Returns(true);
            var queryString = new NameValueCollection { { "ReturnUrl", "/admin/settings" } };
            loginController.SetFakeControllerContext(queryString);

            var actual = loginController.Authenticate(credentials) as RedirectResult;

            Assert.NotNull(actual);
            Assert.That(actual.Url, Is.EqualTo("/admin/settings"));
        }

        [Test]
        public void Authenticate_InvalidCredentials_ReturnsCorrectModel()
        {
            authService.Setup(service => service.Authenticate(credentials.Username, credentials.Password))
                       .Returns(false);

            var actual = loginController.Authenticate(credentials) as ViewResult;

            Assert.NotNull(actual);
            Assert.That(loginController.ModelState[""].Errors[0].ErrorMessage, 
                Is.EqualTo("Username or Password is incorrect."));
        }

        [Test]
        public void Logout_RedirectsToHome()
        {
            var actual = loginController.Logout() as RedirectToRouteResult;

            Assert.NotNull(actual);
            Assert.That(actual.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(actual.RouteValues["controller"], Is.EqualTo("Home"));
        }
    }
}