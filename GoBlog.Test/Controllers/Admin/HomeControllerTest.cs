using AutoMapper;
using GoBlog.Areas.Admin.Controllers;
using GoBlog.Infrastructure.AutoMapper;
using GoBlog.Models;
using GoBlog.Persistence;
using GoBlog.Test.Helpers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Test.Controllers.Admin
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController controller;
        private Mock<IRepository> repository;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.Configure();
            Mapper.AssertConfigurationIsValid();
            repository = RepositoryMockHelper.MockRepository();
            controller= new HomeController(repository.Object);
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
            Assert.That(model.Count(), Is.EqualTo(4));
        }

        [Test]
        public void DeleteDeletesPost()
        {
            var actual = controller.Delete("dynamic-contagion-part-one") as RedirectToRouteResult;
            Assert.NotNull(actual);
            Assert.That(repository.Object.Posts.Count(), Is.EqualTo(3));
        }
    }
}