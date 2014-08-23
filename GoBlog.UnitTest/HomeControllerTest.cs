using GoBlog.Common.Pagination;
using GoBlog.Controllers;
using GoBlog.Domain;
using GoBlog.Domain.Model;
using GoBlog.UnitTest.Support;
using Moq;
using NUnit.Framework;
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

            repository
                .Setup(repo => repo.All())
                .Returns(PostsMother.CreatePosts);
        }

        [Test]
        public void Index_RendersDefaultView()
        {
            controller
                .WithCallTo(c => c.Index(1))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Index_ReturnsCorrectModelType()
        {
            controller
                .WithCallTo(c => c.Index(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<Post>>();
        }

        [Test]
        public void Index_ReturnsModelWithCorrectPageNumber()
        {
            const int PageNumber = 2;
            
            controller
                .WithCallTo(c => c.Index(PageNumber))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<Post>>(actual => actual.PageNumber == PageNumber);
        }

        [Test]
        public void Index_NonExistentPage_RedirectsToLastAvailablePage()
        {
            var actual = controller
                .WithCallTo(c => c.Index(3))
                .ShouldRedirectTo(c => c.Index);

            Assert.AreEqual(2, actual["pageNumber"]);
        }

        [Test]
        public void Index_EmptyRepository_RendersDefaultView()
        {
            repository
                .Setup(repo => repo.All())
                .Returns(Enumerable.Empty<Post>);

            controller
                .WithCallTo(c => c.Index(1))
                .ShouldRenderDefaultView();
        }
    }
}