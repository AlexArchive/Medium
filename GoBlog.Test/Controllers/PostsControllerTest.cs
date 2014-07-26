using GoBlog.AutoMapper;
using GoBlog.Controllers;
using GoBlog.Models;
using GoBlog.Paging;
using GoBlog.Test.Support;
using NUnit.Framework;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Test.Controllers
{
    [TestFixture]
    public class PostsControllerTest
    {
        private PostsController controller;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.Configure();
            controller = new PostsController(RepositoryMockHelper.MockRepository().Object);
        }

        [Test]
        public void Index_ReturnsCorrectView()
        {
            var actual = controller.Index();
            Assert.IsAssignableFrom<ViewResult>(actual);
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
        public void Index_ShouldOrderPostsByPublishDate()
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
            var actual = controller.Post("dynamic-contagion-part-one");
            Assert.IsAssignableFrom<ViewResult>(actual);
        }

        [Test]
        public void Post_Draft_ReturnsNotFound()
        {
            var actual = controller.Post("a-face-made-for-email-part-three") as HttpNotFoundResult;
            Assert.NotNull(actual);
        }

        [Test]
        public void Post_NonExistent_ReturnsNotFound()
        {
            var actual = controller.Post("non-existent-slug") as HttpNotFoundResult;
            Assert.NotNull(actual);
        }

        [Test]
        public void Post_ReturnsCorrectModel()
        {
            var viewResult = (ViewResult) controller.Post("dynamic-contagion-part-one");

            var actual = viewResult.Model as PostViewModel;

            Assert.NotNull(actual);
            Assert.That(actual.Title, Is.EqualTo("Dynamic contagion, part one"));
        }

        [Test]
        public void Post_SomePost_HasTags()
        {
            var viewResult = (ViewResult)controller.Post("continuing-to-an-outer-loop");
            var actual = viewResult.Model as PostViewModel;
            CollectionAssert.IsNotEmpty(actual.Tags);
        }
    }
}
