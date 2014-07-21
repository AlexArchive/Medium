using GoBlog.Controllers;
using GoBlog.Infrastructure.AutoMapper;
using GoBlog.Infrastructure.Paging;
using GoBlog.Models;
using GoBlog.Test.Support;
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
            controller = new HomeController(RepositoryMockHelper.MockRepository().Object);
        }

        [Test]
        public void Index_ReturnsCorrectView()
        {
            var actual = controller.Index() as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Index_ReturnsCorrectModel()
        {
            var viewResult = (ViewResult) controller.Index();
            
            var actual = viewResult.Model as PagedList<PostViewModel>;

            Assert.NotNull(actual);
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual.First().Title, Is.EqualTo("Dynamic contagion, part one"));
        }

        [Test]
        public void Index_PageNumber2_ReturnsCorrectModel()
        {
            var viewResult = (ViewResult) controller.Index(2);

            var actual = viewResult.Model as PagedList<PostViewModel>;

            Assert.NotNull(actual);
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual.First().Title, Is.EqualTo("When should I write a property?"));
        }

        [Test]
        public void Index_PostsShouldBeOrderedByPublishDate()
        {
            var firstPage = (ViewResult) controller.Index();
            var firstPageModel = (PagedList<PostViewModel>)firstPage.Model;
            var lastPage = (ViewResult) controller.Index(firstPageModel.PageCount);
            var lastPageModel = (PagedList<PostViewModel>)lastPage.Model;

            Assert.That(firstPageModel.First().Title, Is.EqualTo("Dynamic contagion, part one"));
            Assert.That(lastPageModel.Last().Title, Is.EqualTo("Lowering in language design, part two"));
        }

        [Test]
        public void Post_ReturnsCorrectView()
        {
            var actual = controller.Post("dynamic-contagion-part-one") as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Post"));
        }

        [Test]
        public void Post_ReturnsCorrectModel()
        {
            var viewResult = (ViewResult) controller.Post("dynamic-contagion-part-one");

            var actual = viewResult.Model as PostViewModel;

            Assert.NotNull(actual);
            Assert.That(actual.Title, Is.EqualTo("Dynamic contagion, part one"));
        }
    }
}
