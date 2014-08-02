using GoBlog.AutoMapper;
using GoBlog.Controllers;
using GoBlog.Data;
using GoBlog.Data.Entities;
using GoBlog.Models;
using GoBlog.Paging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Web.Mvc;

namespace GoBlog.Test
{
    [TestFixture]
    public class PostsControllerTest
    {
        private PostsController controller;
        private Mock<IPostsRepository> repository;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.Configure();
            repository = new Mock<IPostsRepository>();
            controller = new PostsController(repository.Object);
        }

        [Test]
        public void Index_ReturnsCorrectViewName()
        {
            var actual = controller.Index();

            Assert.AreEqual(actual.ViewName, "Index");
        }

        [Test]
        public void Index_ReturnsCorrectModel()
        {
            var result = controller.Index();
            var actual = result.Model as PagedList<PostViewModel>;

            Assert.NotNull(actual);
        }

        [Test]
        public void Index_WithPageNumber_ReturnsCorrectViewName()
        {
            var actual = controller.Index(2);

            Assert.AreEqual(actual.ViewName, "Index");
        }

        [Test]
        public void Index_WithPageNumber_ReturnsCorrectModel()
        {
            var result = controller.Index(2);
            var actual = result.Model;

            Assert.IsAssignableFrom<PagedList<PostViewModel>>(actual);
        }

        [Test]
        public void Post_ReturnsCorrectViewName()
        {
            repository.Setup(repo => repo.Find("arbitrary-slug"))
                      .Returns(new Post());

            var actual = controller.Post("arbitrary-slug") as ViewResult;

            Assert.NotNull(actual);
            Assert.AreEqual("Post", actual.ViewName);
        }

        [Test]
        public void Post_ThatIsDraft_ReturnsNotFound()
        {
            var post = new Post { Draft = true };
            repository.Setup(repo => repo.Find("arbitrary-slug"))
                      .Returns(post);

            var actual = controller.Post("arbitrary-slug");

            Assert.IsAssignableFrom<HttpNotFoundResult>(actual);
        }

        [Test]
        public void Post_ThatDoesNotExist_ReturnNotFound()
        {
            var actual = controller.Post("arbitrary-slug");

            Assert.IsAssignableFrom<HttpNotFoundResult>(actual);
        }

        [Test]
        public void Post_ReturnsCorrectModel()
        {
            var post = new Post
            {
                Slug = "continuing-to-an-outer-loop",
                Title = "Continuing to an outer loop",
                Summary = "When you have a nested loop, sometimes",
                Content = "When you have a nested loop, sometimes",
                PublishedAt = DateTime.Now.AddDays(7),
                Tags = new Collection<Tag> { new Tag { Name = "Programming" } }
            };

            repository.Setup(repo => repo.Find("continuing-to-an-outer-loop"))
                      .Returns(post);

            var viewResult = (ViewResult) controller.Post("continuing-to-an-outer-loop");
            var actual = viewResult.Model as PostViewModel;

            Assert.NotNull(actual);
            Assert.AreEqual(actual.Slug, post.Slug);
            Assert.AreEqual(actual.Title, post.Title);
            Assert.AreEqual(actual.Summary, post.Summary);
            Assert.AreEqual(actual.Content, post.Content);
            Assert.AreEqual(actual.PublishedAt, post.PublishedAt);
            Assert.AreEqual(actual.Tags, post.Tags);
        }
    }
}