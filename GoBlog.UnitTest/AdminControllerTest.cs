using AutoMapper;
using GoBlog.Areas.Admin.Controllers;
using GoBlog.Areas.Admin.Models;
using GoBlog.Domain;
using GoBlog.Domain.Model;
using GoBlog.UnitTest.Support;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace GoBlog.UnitTest
{
    [TestFixture]
    public class AdminControllerTest
    {
        private AdminController controller;
        private Mock<IPostsRepository> repository;
        private Mock<IMappingEngine> mapper;
        
        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IPostsRepository>();
            mapper = new Mock<IMappingEngine>();
            controller = new AdminController(repository.Object, mapper.Object);
        }

        [Test]
        public void Index_RendersDefaultView()
        {
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Index_ReturnsCorrectModelType()
        {
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<Post>>();
        }

        [Test]
        public void Index_ReturnsCorrectModel()
        {
            const int PostCount = 1;
            repository
                .Setup(repo => repo.All())
                .Returns(PostMother.CreatePosts(PostCount));

            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<Post>>(actual => actual.Count() == PostCount);
        }

        [Test]
        public void Delete_RedirectsToIndex()
        {
            controller
                .WithCallTo(c => c.Delete(""))
                .ShouldRedirectTo(c => c.Index);
        }

        [Test]
        public void Delete_ExistentPost_ReturnsCorrectMessage()
        {
            const string Slug = "";
            repository
                .Setup(repo => repo.Delete(Slug))
                .Returns(true);

            controller.Delete(Slug);

            var message = controller.TempData["Message"].ToString();
            Assert.That(message.Contains("success"));
        }

        [Test]
        public void Delete_NonExistentPost_ReturnsCorrectMessage()
        {
            controller.Delete("");

            var message = controller.TempData["Message"].ToString();
            Assert.That(message.Contains("no longer exists"));
        }

        [Test]
        public void Add_RendersDefaultView()
        {
            controller
                .WithCallTo(c => c.Add())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Add_ValidModel_RedirectsToEdit()
        {
            var post = PostInputMother.CreatePost();

            controller
                .WithCallTo(c => c.Add(post))
                .ShouldRedirectTo(c => c.Edit(""));
        }

        [Test]
        public void Add_InvalidModel_ReturnsModelError()
        {
            var post = PostInputMother.CreatePost();
            repository
                .Setup(r => r.Add(It.IsAny<Post>()))
                .Throws<Exception>();

            controller
                .WithCallTo(c => c.Add(post))
                .ShouldRenderDefaultView()
                .WithModel<PostInputModel>()
                .AndModelError("")
                .Containing("You have previously published a post with this title.");
        }

        [Test]
        public void Add_CallsAdd()
        {
            var postModel = PostInputMother.CreatePost();
            var postEntity = PostMother.CreatePost();
            mapper.Setup(m => m.Map<Post>(postModel)).Returns(postEntity);

            controller.Add(postModel);

            repository.Verify(r => r.Add(postEntity), Times.Once);
        }

        [Test]
        public void Edit_RendersDefaultView()
        {
            controller
                .WithCallTo(c => c.Edit(""))
                .ShouldRenderDefaultView();
        }
    }
}