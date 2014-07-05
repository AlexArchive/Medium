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

        [TestFixtureSetUp]
        public void SetUp()
        {
            AutoMapperConfig.Configure();
            var repository = RepositoryMockHelper.MockRepository();
            controller = new HomeController(repository.Object);
        }

        [Test]
        public void IndexReturnsCorrectView()
        {
            // act
            var actual = controller.Index() as ViewResult;

            // assert
            Assert.NotNull(actual);
            Assert.AreEqual("Index", actual.ViewName);
        }

        [Test]
        public void IndexReturnsCorrectModel()
        {
            // act
            var actual = controller.Index() as ViewResult;
            var model = actual.Model as PagedList<PostViewModel>;

            // assert
            Assert.NotNull(model);
            Assert.That(model.Count == 2);
            Assert.That(model.First().Title == "Dynamic contagion, part one");
        }

        [Test]
        public void IndexReturnsCorrectModelForPage2()
        {
            // act
            var actual = controller.Index(2) as ViewResult;
            var model = actual.Model as PagedList<PostViewModel>;

            // assert
            Assert.NotNull(model);
            Assert.That(model.Count == 2);
            Assert.That(model.First().Title == "When should I write a property?");
        }

        [Test]
        public void IndexShouldOrderPostsByPublishDate()
        {
            // act
            var firstPage = (ViewResult) controller.Index();
            var firstPageModel = (PagedList<PostViewModel>) firstPage.Model;
            
            var lastPage = (ViewResult)controller.Index(firstPageModel.PageCount);
            var lastPageModel = (PagedList<PostViewModel>)lastPage.Model;

            // assert
            Assert.That(firstPageModel.First().Title == "Dynamic contagion, part one");
            Assert.That(lastPageModel.Last().Title == "Lowering in language design, part two");
        }

        [Test]
        public void PostReturnsCorrectView()
        {
            // act
            var actual = controller.Post("dynamic-contagion-part-one") as ViewResult;

            // assert
            Assert.NotNull(actual);
            Assert.AreEqual("Post", actual.ViewName);
        }
        
        [Test]
        public void PostReturnsCorrectModel()
        {
            // act
            var actual = controller.Post("dynamic-contagion-part-one") as ViewResult;
            var model = actual.Model as PostViewModel;

            // assert
            Assert.NotNull(model);
            Assert.AreEqual("Dynamic contagion, part one", model.Title);
        }
    }
}
