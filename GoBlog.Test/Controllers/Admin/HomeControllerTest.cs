using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
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
        private PostInputModel postInputModel;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.Configure();
            repository = RepositoryMockHelper.MockRepository();
            controller = new HomeController(repository.Object);
            postInputModel = new PostInputModel
            {
                Slug = "dynamic-contagion-part-one",
                Title = "Dynamic Contagion Part One",
                Content = "Edited content."
            };
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
            var actual = viewResult.Model as IEnumerable<PostViewModel>;

            Assert.NotNull(actual);
            Assert.That(actual.Count(), Is.EqualTo(4));
        }

        [Test]
        public void DeleteDeletesPost()
        {
            var actual = controller.Delete("dynamic-contagion-part-one") as RedirectToRouteResult;

            Assert.NotNull(actual);
            Assert.That(actual.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(repository.Object.Posts.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DeleteReturns404WhenPostDoesntExist()
        {
            var actual = controller.Delete("non-existent-slug") as HttpNotFoundResult;

            Assert.NotNull(actual);
            Assert.That(actual.StatusDescription, Is.EqualTo("You cannot delete a post that does not exist."));
        }

        [Test]
        public void EditReturnsCorrectView()
        {
            var actual = controller.Edit("dynamic-contagion-part-one") as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void EditReturnsCorrectModel()
        {
            var viewResult = controller.Edit("dynamic-contagion-part-one") as ViewResult;

            var actual = viewResult.Model as PostInputModel;

            Assert.NotNull(actual);
            Assert.That(actual.Title, Is.EqualTo("Dynamic contagion, part one"));
        }

        [Test]
        public void EditReturns404WhenPostDoesntExist()
        {
            var actual = controller.Edit("non-existent-slug") as HttpNotFoundResult;

            Assert.NotNull(actual);
            Assert.That(actual.StatusDescription, Is.EqualTo("You cannot edit a post that does not exist."));
        }

        [Test]
        public void EditPostReturnsCorrectView()
        {
            var actual = controller.Edit(postInputModel) as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void EditPostEditsContent()
        {
            postInputModel.Content = "sample content.";
            var actual = controller.Edit(postInputModel) as ViewResult;

            var post = repository.Object.Posts.First(p => p.Slug == "dynamic-contagion-part-one");
            Assert.That(post.Content, Is.EqualTo("sample content."));
        }

        [TestCase("test", ExpectedResult = "test")]
        [TestCase("test\r\ntest", ExpectedResult = "test")]
        public string EditPostEditsSummary(string content)
        {
            postInputModel.Content = content;

            var actual = controller.Edit(postInputModel) as ViewResult;

            var post = repository.Object.Posts.First(p => p.Slug == "dynamic-contagion-part-one");
            return post.Summary;
        }

        [Test]
        public void EditPostEditsSlug()
        {
            postInputModel.Title = "Hello";

            var actual = controller.Edit(postInputModel) as ViewResult;

            Assert.DoesNotThrow(() =>
                repository.Object.Posts.First(p => p.Slug == "hello"));
        }

        [Test]
        public void EditPostReturns404WhenPostDoesntExist()
        {
            var model = new PostInputModel { Title = "Non Existent Slug", Slug = "non-existent-slug" };

            var actual = controller.Edit(model) as HttpNotFoundResult;

            Assert.NotNull(actual);
            Assert.That(actual.StatusDescription, Is.EqualTo("You cannot edit a post that does not exist."));
        }

        [Test]
        public void EditPostReturnsCorrectViewWhenSuppliedWithInvalidModel()
        {
            controller.ModelState.AddModelError("", "");
            var model = new PostInputModel();

            var actual = controller.Edit(model) as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
            Assert.AreEqual(model, actual.Model);
        }

        [Test]
        public void Edit_WithOccupiedSlug_ReturnsCorrectView()
        {
            var inputModel = new PostInputModel
            {
                Title = "Lowering in language design, part two",
                Content = @"Who cares"
            };

            var actual = controller.Edit(inputModel) as ViewResult;
            Assert.NotNull(actual);
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage == "You have previously published a post with this title. Please choose another one.");
        }

        [Test]
        public void Add_ReturnsCorrectView()
        {
            var actual = controller.Add() as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void Add_SavesPost()
        {
            var inputModel = new PostInputModel
            {
                Title = "Copy-paste defects",
                Content = @"Continuing with my series of answers to questions that were 
                            asked during my webcast on Tuesday:"
            };

            controller.Add(inputModel);

            var actual = repository.Object.Posts.SingleOrDefault(post => post.Slug == "copy-paste-defects");
            Assert.NotNull(actual);
        }

        [Test]
        public void Add_Post_ReturnsCorrectView()
        {
            var inputModel = new PostInputModel
            {
                Title = "Copy-paste defects",
                Content = @"Continuing with my series of answers to questions that were 
                            asked during my webcast on Tuesday:"
            };

            var actual = controller.Add(inputModel) as RedirectToRouteResult;
            Assert.NotNull(actual);
            Assert.That(actual.RouteValues["action"], Is.EqualTo("Edit"));
            Assert.That(controller.TempData["newPost"], Is.True);
        }

        [Test]
        public void Add_WithOccupiedSlug_ReturnsCorrectView()
        {
            var inputModel = new PostInputModel
            {
                Title = "Lowering in language design, part two",
                Content = @"Who cares"
            };

            var actual = controller.Add(inputModel) as ViewResult;
            Assert.NotNull(actual);
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage == "You have previously published a post with this title. Please choose another one.");
        }

        [TestCase("test", ExpectedResult = "test")]
        [TestCase("test\r\ntest", ExpectedResult = "test")]
        public string AddPostEditsSummary(string content)
        {
            PostInputModel model = new PostInputModel { Title = "does", Content = content };
            var actual = controller.Add(model) as ViewResult;
            var post = repository.Object.Posts.First(p => p.Slug == "does");
            return post.Summary;
        }
    }
}