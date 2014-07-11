using GoBlog.Controllers;
using GoBlog.Infrastructure.AutoMapper;
using GoBlog.Infrastructure.Paging;
using GoBlog.Models;
using GoBlog.Test.Helpers;
using NUnit.Framework;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Test.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.Configure();
            var repository = RepositoryMockHelper.MockRepository();
            controller = new HomeController(repository.Object);
        }

        [Test]
        public void IndexReturnsCorrectView()
        {
            var actual = controller.Index() as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void IndexReturnsCorrectModel()
        {
            var viewResult = controller.Index() as ViewResult;
            var actual = viewResult.Model as PagedList<PostViewModel>;

            Assert.NotNull(actual);
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual.First().Title, Is.EqualTo("Dynamic contagion, part one"));
        }

        [Test]
        public void IndexReturnsCorrectModelForPage2()
        {
            var viewResult = controller.Index(2) as ViewResult;
            var actual = viewResult.Model as PagedList<PostViewModel>;

            Assert.NotNull(actual);
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual.First().Title, Is.EqualTo("When should I write a property?"));
        }

        [Test]
        public void IndexShouldOrderPostsByPublishDate()
        {
            var firstPage = (ViewResult) controller.Index();
            var firstPageModel = (PagedList<PostViewModel>) firstPage.Model;

            var lastPage = (ViewResult) controller.Index(firstPageModel.PageCount);
            var lastPageModel = (PagedList<PostViewModel>) lastPage.Model;

            Assert.That(firstPageModel.First().Title, Is.EqualTo("Dynamic contagion, part one"));
            Assert.That(lastPageModel.Last().Title, Is.EqualTo("Lowering in language design, part two"));
        }

        [Test]
        public void SinglePostReturnsCorrectView()
        {
            var actual = controller.Post("dynamic-contagion-part-one") as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Post"));
        }

        [Test]
        public void SinglePostReturnsCorrectModel()
        {
            var viewResult = controller.Post("dynamic-contagion-part-one") as ViewResult;
            var actual = viewResult.Model as PostViewModel;

            Assert.NotNull(actual);
            Assert.That( actual.Title, Is.EqualTo("Dynamic contagion, part one"));
        }
    }
}
