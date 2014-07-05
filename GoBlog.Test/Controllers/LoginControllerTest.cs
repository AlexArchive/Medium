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
        private Mock<IAuthenticationHandler> authHandler;
        private LoginController loginController;

        [SetUp]
        public void SetUp()
        {
            authHandler = new Mock<IAuthenticationHandler>();
            loginController = new LoginController(authHandler.Object);
            loginController.SetFakeControllerContext();
            credentials = new CredentialsModel { Username = "admin", Password = "password" };
        }

        [Test]
        public void CorrectCredentials()
        {
            // arrange
            authHandler.Setup(handler => handler.Authenticate("admin", "password")).Returns(true);

            // act
            var result = loginController.Authenticate(credentials) as RedirectResult;

            // assert
            Assert.NotNull(result);
            Assert.That(result.Url, Is.EqualTo("/admin"));
        }

        [Test]
        public void CorrectCredentialsWithReturnUrl()
        {
            // arrange
            authHandler.Setup(handler => handler.Authenticate("admin", "password")).Returns(true);
            var queryParams = new NameValueCollection { { "ReturnUrl", "/admin/settings" } };
            loginController.SetFakeControllerContext(queryParams);

            // act
            var result = loginController.Authenticate(credentials) as RedirectResult;

            // assert
            Assert.NotNull(result);
            Assert.That(result.Url, Is.EqualTo("/admin/settings"));
        }

        [Test]
        public void WrongCredentials()
        {
            // arrange
            authHandler.Setup(handler => handler.Authenticate("admin", "password")).Returns(false);

            // act
            var result = loginController.Authenticate(credentials) as ViewResult;

            // assert
            Assert.NotNull(result);
            Assert.That(loginController.ModelState[""].Errors[0].ErrorMessage == "Username or Password is incorrect.");
        }

        // test ideas:
        //  missing username?
        //  missing password?
        //  is password case sensitive?
        //  is username case sensitive?
        //  already authenticated?
    }
}