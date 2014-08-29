using GoBlog.Areas.Admin.Controllers;
using GoBlog.Domain;
using GoBlog.Domain.Model;
using GoBlog.UnitTest.Support;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace GoBlog.UnitTest
{
    [TestFixture]
    public class AdminControllerTest
    {
        private AdminController controller;
        private Mock<IPostsRepository> repository;
        
        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IPostsRepository>();
            controller = new AdminController(repository.Object);
        }

        [Test]
        public void Index_RendersDefaultView()
        {
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Index_ReturnsCorrectModelType()
        {
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<Post>>();
        }

        [Test]
        public void Index_ReturnsCorrectModel()
        {
            const int PostCount = 1;
            repository
                .Setup(repo => repo.All())
                .Returns(PostsMother.CreateEmptyPosts(PostCount));

            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<Post>>(actual => actual.Count() == PostCount);
        }

        [Test]
        public void Delete_RedirectsToIndex()
        {
            controller
                .WithCallTo(c => c.Delete(""))
                .ShouldRedirectTo(c => c.Index);
        }

        [Test]
        public void Delete_ExistentPost_ReturnsCorrectMessage()
        {
            const string Slug = "";
            repository
                .Setup(repo => repo.Delete(Slug)).Returns(true);

            controller.Delete(Slug);

            var message = controller.TempData["Message"].ToString();
            Assert.That(message.Contains("success"));
        }

        [Test]
        public void Delete_NonExistentPost_ReturnsCorrectMessage()
        {
            controller.Delete("");

            var message = controller.TempData["Message"].ToString();
            Assert.That(message.Contains("no longer exists"));
        }
    }
}