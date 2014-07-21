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
            Assert.That(actual.Count, Is.EqualTo(8));
            Assert.That(actual.First().Title, Is.EqualTo("Dynamic contagion, part one"));
        }

        [Test]
        public void Index_PageNumber2_ReturnsCorrectModel()
        {
            var viewResult = (ViewResult) controller.Index(2);

            var actual = viewResult.Model as PagedList<PostViewModel>;

            Assert.NotNull(actual);
            Assert.That(actual.Count, Is.EqualTo(1));
        }

        [Test]
        public void Index_PostsShouldBeOrderedByPublishDate()
        {
            var viewResult = controller.Index() as ViewResult;
            var actual = viewResult.Model as PagedList<PostViewModel>;
            var sorted = actual.OrderBy(post => post.PublishedAt);
            CollectionAssert.AreEqual(sorted, actual);
        }

        [Test]
        public void Index_ExcludesDrafts()
        {
        }

        [Test]
        public void Post_ReturnsCorrectView()
        {
            var actual = controller.Post("dynamic-contagion-part-one") as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Post"));
        }

        [Test]
        public void Post_Draft_ReturnsNotFound()
        {
            var actual = controller.Post("a-face-made-for-email-part-three") as HttpNotFoundResult;
            Assert.NotNull(actual);
            Assert.That(actual.StatusDescription, Is.EqualTo("You cannot view this post because it is a draft."));
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
