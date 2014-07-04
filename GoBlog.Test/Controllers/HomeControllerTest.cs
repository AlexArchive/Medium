using GoBlog.Controllers;
using GoBlog.Infrastructure.AutoMapper;
using GoBlog.Models;
using GoBlog.Test.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
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
            var model = actual.Model as IEnumerable<PostViewModel>;

            // assert
            Assert.NotNull(model);
            Assert.That(model.First().Title == "Dynamic contagion, part one");
        }
    }
}
