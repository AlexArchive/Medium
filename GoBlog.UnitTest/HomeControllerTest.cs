using GoBlog.Controllers;
using GoBlog.Domain;
using GoBlog.Domain.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace GoBlog.UnitTest
{
    [TestFixture]
    public class HomeControllerTest
    {
        private Mock<IPostsRepository> repository;
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IPostsRepository>();
            controller = new HomeController(repository.Object); 
        }

        [Test]
        public void Index_RendersCorrectView()
        {
            controller.WithCallTo(c => c.Index())
                      .ShouldRenderDefaultView();
        }

        [Test]
        public void Index_ReturnsCorrectModelType()
        {
            controller.WithCallTo(c => c.Index())
                      .ShouldRenderDefaultView()
                      .WithModel<IEnumerable<Post>>();
        }

        [Test]
        public void Index_EmptyRepository_ReturnsNoPosts()
        {
            controller.WithCallTo(c => c.Index())
                      .ShouldRenderDefaultView()
                      .WithModel<IEnumerable<Post>>(actual => actual.Count() == 0);
        }

        [Test]
        public void Index_ReturnsAllPosts()
        {
            var posts = Enumerable.Repeat(new Post(), 5);
            repository.Setup(r => r.All()).Returns(posts);

            controller.WithCallTo(c => c.Index())
                      .ShouldRenderDefaultView()
                      .WithModel<IEnumerable<Post>>(actual => actual.Count() == posts.Count());
        }
    }
}