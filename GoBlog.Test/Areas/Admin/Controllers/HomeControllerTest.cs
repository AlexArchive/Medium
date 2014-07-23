using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
using GoBlog.Infrastructure.AutoMapper;
using GoBlog.Models;
using GoBlog.Persistence;
using GoBlog.Test.Support;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Test.Areas.Admin.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController controller;
        private Mock<IRepository> repository;
        private PostInputModel post;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.Configure();
            repository = RepositoryMockHelper.MockRepository();
            controller = new HomeController(repository.Object);
            post = new PostInputModel
            {
                Slug = "dynamic-contagion-part-one",
                Title = "Dynamic Contagion Part One",
                Content = "Edited content."
            };
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
            var viewResult = (ViewResult)controller.Index();
            var actual = viewResult.Model as IEnumerable<PostViewModel>;

            Assert.NotNull(actual);
            Assert.That(actual.Count(), Is.EqualTo(repository.Object.Posts.Count()));
        }

        [Test]
        public void Delete_DeletesPost()
        {
            var actual = controller.Delete("dynamic-contagion-part-one") as RedirectToRouteResult;

            Assert.NotNull(actual);
            Assert.That(actual.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(repository.Object.Posts.Count(), Is.EqualTo(9));
        }

        [Test]
        public void Delete_NonExistentPost_ReturnsNotFound()
        {
            var actual = controller.Delete("non-existent-slug") as HttpNotFoundResult;

            Assert.NotNull(actual);
            Assert.That(actual.StatusDescription, Is.EqualTo("You cannot delete a post that does not exist."));
        }

        [Test]
        public void Edit_ReturnsCorrectView()
        {
            var actual = controller.Edit("dynamic-contagion-part-one") as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void Edit_ReturnsCorrectModel()
        {
            var viewResult = (ViewResult)controller.Edit("dynamic-contagion-part-one");
            var actual = viewResult.Model as PostInputModel;

            Assert.NotNull(actual);
            Assert.That(actual.Title, Is.EqualTo("Dynamic contagion, part one"));
        }

        [Test]
        public void Edit_ReturnsModelWithTags()
        {
            var viewResult = (ViewResult)controller.Edit("continuing-to-an-outer-loop");
            var actual = viewResult.Model as PostInputModel;
            Assert.That(actual.Tags, Is.EqualTo("Programming"));
        }

        [Test]
        public void Edit_NonExistentPost_ReturnsNotFound()
        {
            var actual = controller.Edit("non-existent-slug") as HttpNotFoundResult;

            Assert.NotNull(actual);
            Assert.That(actual.StatusDescription, Is.EqualTo("You cannot edit a post that does not exist."));
        }

        [Test]
        public void Edit_ValidModel_ReturnsCorrectView()
        {
            var actual = controller.Edit(post) as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void Edit_EditsPostContent()
        {
            post.Content = "arbitrary content.";
            controller.Edit(post);

            var amendedPost = repository.Object.Posts.First(p => p.Slug == "dynamic-contagion-part-one");
            Assert.That(amendedPost.Content, Is.EqualTo("arbitrary content."));
        }

        [Test]
        public void Edit_EditsDraftStatus()
        {
            post.Draft = true;
            controller.Edit(post);

            var amendedPost = repository.Object.Posts.First(p => p.Slug == "dynamic-contagion-part-one");
            Assert.True(amendedPost.Draft);
        }

        [TestCase("foo", ExpectedResult = "foo")]
        [TestCase("foo\r\nbar", ExpectedResult = "foo")]
        public string Edit_EditsPostSummary(string content)
        {
            post.Content = content;
            controller.Edit(post);

            var amendedPost = repository.Object.Posts.First(p => p.Slug == "dynamic-contagion-part-one");
            return amendedPost.Summary;
        }

        [Test]
        public void Edit_EditsTags()
        {
            post.Tags = "Programming";
            controller.Edit(post);

            var actual = repository.Object.Posts.First(p => p.Slug == post.Slug);

            var expected = new[] { "Programming" };
            CollectionAssert.AreEquivalent(expected, actual.Tags.Select(x => x.Name));
        }

        [Test]
        public void Edit_EditsPostSlug()
        {
            post.Title = "Hello, World";
            controller.Edit(post);

            repository.Object.Posts.First(p => p.Slug == "hello-world");
        }

        [Test]
        public void Edit_Post_NonExistentPost_ReturnsNotFound()
        {
            var model =
                new PostInputModel { Title = "Non Existent Slug", Slug = "non-existent-slug" };

            var actual = controller.Edit(model) as HttpNotFoundResult;

            Assert.NotNull(actual);
            Assert.That(actual.StatusDescription, Is.EqualTo("You cannot edit a post that does not exist."));
        }

        [Test]
        public void Edit_InvalidModel_ReturnsCorrectModel()
        {
            controller.ModelState.AddModelError("", "");
            var model = new PostInputModel();

            var actual = controller.Edit(model) as ViewResult;

            Assert.NotNull(actual);
            Assert.That(actual.ViewName, Is.EqualTo("Edit"));
            Assert.AreEqual(model, actual.Model);
        }

        [Test]
        public void Edit_OccupiedSlug_ReturnsCorrectView()
        {
            var model = new PostInputModel
            {
                Title = "Lowering in language design, part two",
                Content = "arbitrary content."
            };

            var actual = controller.Edit(model) as ViewResult;

            Assert.NotNull(actual);
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage,
                Is.EqualTo("You have previously published a post with this title. Please choose another one."));
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
            var newPost = new PostInputModel
            {
                Title = "Copy-paste defects",
                Content = @"Continuing with my series of answers to questions that were 
                            asked during my webcast on Tuesday:"
            };

            controller.Add(newPost);
            repository.Object.Posts.Single(p => p.Slug == "copy-paste-defects");
        }

        [Test]
        public void Add_WithTags_SavesPost()
        {
            var newPost = new PostInputModel
            {
                Title = "Copy-paste defects",
                Content = @"Continuing with my series of answers to questions that were 
                            asked during my webcast on Tuesday:",
                Tags = "Programming, General"
            };

            controller.Add(newPost);
            var addedPost = repository.Object.Posts.Single(p => p.Slug == "copy-paste-defects");
            Assert.That(addedPost.Tags.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Add_WithExistingTags_SavesPost()
        {
            var newPost = new PostInputModel
            {
                Title = "Copy-paste defects",
                Content = @"Continuing with my series of answers to questions that were 
                            asked during my webcast on Tuesday:",
                Tags = "Programming"
            };
            controller.Add(newPost);
            newPost.Slug = "something";
            controller.Add(newPost);
        }

        [Test]
        public void Add_Draft_SavesPost()
        {
            var newPost = new PostInputModel
            {
                Title = "Copy-paste defects",
                Content = @"Continuing with my series of answers to questions that were 
                            asked during my webcast on Tuesday:",
                Draft = true
            };

            controller.Add(newPost);
            var addedPost = repository.Object.Posts.Single(p => p.Slug == "copy-paste-defects");
            Assert.True(addedPost.Draft);
        }

        [Test]
        public void Add_ValidModel_RedirectsToEdit()
        {
            var newPost = new PostInputModel
            {
                Title = "Copy-paste defects",
                Content = @"Continuing with my series of answers to questions that were 
                            asked during my webcast on Tuesday:"
            };

            var actual = controller.Add(newPost) as RedirectToRouteResult;
            Assert.NotNull(actual);
            Assert.That(actual.RouteValues["action"], Is.EqualTo("Edit"));
            Assert.That(controller.TempData["newPost"], Is.True);
        }

        [Test]
        public void Add_OccupiedSlug_ReturnsCorrectView()
        {
            var inputModel = new PostInputModel
            {
                Title = "Lowering in language design, part two",
                Content = @"Who cares"
            };

            var actual = controller.Add(inputModel) as ViewResult;
            Assert.NotNull(actual);
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage,
                Is.EqualTo("You have previously published a post with this title. Please choose another one."));
        }

        [TestCase("foo", ExpectedResult = "foo")]
        [TestCase("foo\r\nbar", ExpectedResult = "foo")]
        public string AddPostEditsSummary(string content)
        {
            PostInputModel model = new PostInputModel { Title = "does", Content = content };
            var actual = controller.Add(model) as ViewResult;
            var post = repository.Object.Posts.First(p => p.Slug == "does");
            return post.Summary;
        }


    }
}