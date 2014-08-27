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
    }
}