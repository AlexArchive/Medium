using GoBlog.Common.Pagination;
using GoBlog.Controllers;
using GoBlog.Domain;
using GoBlog.Domain.Model;
using GoBlog.UnitTest.Support;
using Moq;
using NUnit.Framework;
using System.Net;
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
        public void Index_RendersDefaultView()
        {
            repository
                .Setup(repo => repo.AllPosts())
                .Returns(PostMother.CreatePosts());

            controller
                .WithCallTo(c => c.Index(1))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Index_ReturnsCorrectModelType()
        {
            repository
                .Setup(repo => repo.AllPosts())
                .Returns(PostMother.CreatePosts());

            controller
                .WithCallTo(c => c.Index(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<Post>>();
        }

        [Test]
        public void Index_ReturnsModelWithCorrectPageNumber()
        {
            const int PageNumber = 2;
            const int Count = HomeController.PageSize + 1;
            repository
                .Setup(repo => repo.AllPosts())
                .Returns(PostMother.CreatePosts(Count));

            controller
                .WithCallTo(c => c.Index(PageNumber))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<Post>>(actual => actual.PageNumber == PageNumber);
        }

        [Test]
        public void Index_NonExistentPage_RedirectsToLastAvailablePage()
        {
            const int Count = HomeController.PageSize + 1;
            repository
                .Setup(repo => repo.AllPosts())
                .Returns(PostMother.CreatePosts(Count));

            var actual = controller
                .WithCallTo(c => c.Index(3))
                .ShouldRedirectTo(c => c.Index);

            Assert.AreEqual(2, actual["pageNumber"]);
        }

        [Test]
        public void Index_EmptyRepository_RendersDefaultView()
        {
            controller
                .WithCallTo(c => c.Index(1))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Post_RendersDefaultView()
        {
            const string Slug = "abc";
            repository
                .Setup(repo => repo.FindPost(Slug))
                .Returns(new Post());

            controller
                .WithCallTo(c => c.Post(Slug))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Post_ReturnsCorrectModelType()
        {
            const string Slug = "abc";
            repository
                .Setup(repo => repo.FindPost(Slug))
                .Returns(new Post());

            controller
                .WithCallTo(c => c.Post(Slug))
                .ShouldRenderDefaultView()
                .WithModel<Post>();
        }

        [Test]
        public void Post_ReturnsCorrectModel()
        {
            const string Slug = "abc";
            repository
               .Setup(repo => repo.FindPost(Slug))
               .Returns(PostMother.CreatePost(withSlug: Slug));

            controller
                .WithCallTo(c => c.Post(Slug))
                .ShouldRenderDefaultView()
                .WithModel<Post>(actual => actual.Slug == Slug);
        }

        [Test]
        public void Post_NonExistentPost_ReturnsNotFound()
        {
            controller
                .WithCallTo(c => c.Post("abc"))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }
    }
}