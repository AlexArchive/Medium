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
using System.Net;
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
                .Setup(repo => repo.AllPosts())
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
                .Setup(repo => repo.RemovePost(Slug))
                .Returns(true);

            controller.Delete(Slug);

            StringAssert.Contains("success", controller.TempData["Message"] as string);
        }

        [Test]
        public void Delete_NonExistentPost_ReturnsCorrectMessage()
        {
            controller.Delete("");

            StringAssert.Contains("no longer exists", controller.TempData["Message"] as string);
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
            const string Slug = "abc";
            var post = PostInputMother.CreatePost();
            mapper
                .Setup(m => m.Map<Post>(post))
                .Returns(PostMother.CreatePost(withSlug: Slug));

            controller
                .WithCallTo(c => c.Add(post))
                .ShouldRedirectTo(c => c.Edit(Slug))
                .WithRouteValue("slug", Slug);
        }

        [Test]
        public void Add_ValidModel_ReturnsCorrectMessage()
        {
            var post = PostInputMother.CreatePost();
            mapper
                .Setup(m => m.Map<Post>(post))
                .Returns(PostMother.CreatePost());

            controller.Add(post);

            Assert.True((bool) controller.TempData["newPost"]);
        }

        [Test]
        public void Add_InvalidModel_ReturnsModelError()
        {
            var post = PostInputMother.CreatePost();
            repository
                .Setup(r => r.AddPost(It.IsAny<Post>()))
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
            mapper
                .Setup(m => m.Map<Post>(postModel))
                .Returns(postEntity);

            controller.Add(postModel);

            repository.Verify(r => r.AddPost(postEntity), Times.Once);
        }

        [Test]
        public void Edit_NonExistentPost_ReturnsNotFound()
        {
            controller
                .WithCallTo(c => c.Edit(""))
                .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [Test]
        public void Edit_ExistentPost_RendersDefaultView()
        {
            const string Slug = "";
            repository
                .Setup(repo => repo.FindPost(Slug))
                .Returns(PostMother.CreatePost());

            controller
                .WithCallTo(c => c.Edit(Slug))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Edit_ExistentPost_ReturnsCorrectModelType()
        {
            const string Slug = "";
            var post = PostMother.CreatePost();
            repository
                .Setup(repo => repo.FindPost(Slug))
                .Returns(post);
            mapper
                .Setup(m => m.Map<PostInputModel>(post))
                .Returns(PostInputMother.CreatePost());

            controller
                .WithCallTo(c => c.Edit(Slug))
                .ShouldRenderDefaultView()
                .WithModel<PostInputModel>();
        }

        [Test]
        public void Edit_ExistentPost_CallsMapper()
        {
            const string Slug = "";
            var post = PostMother.CreatePost();
            repository
                .Setup(repo => repo.FindPost(Slug))
                .Returns(post);

            controller.Edit(Slug);

            mapper.Verify(m => m.Map<PostInputModel>(post), Times.Once);
        }

        [Test]
        public void Edit_ValidModel_RedirectsToEdit()
        {
            const string Slug = "";
            var post = PostInputMother.CreatePost();
            mapper
                .Setup(m => m.Map<Post>(post))
                .Returns(PostMother.CreatePost(withSlug: Slug));

            controller
                .WithCallTo(c => c.Edit(post))
                .ShouldRedirectTo(c => c.Edit(post.Slug))
                .WithRouteValue("slug", Slug);
        }

        [Test]
        public void Edit_CallsEdit()
        {
            var postInputModel = PostInputMother.CreatePost();
            var post = PostMother.CreatePost();
            mapper
                .Setup(m => m.Map<Post>(postInputModel))
                .Returns(post);

            controller.Edit(postInputModel);

            repository.Verify(r => r.UpdatePost(post), Times.Once);
        }

        [Test]
        public void Edit_InvalidModel_ReturnsModelError()
        {
            var post = PostInputMother.CreatePost();
            repository
                .Setup(r => r.UpdatePost(It.IsAny<Post>()))
                .Throws<Exception>();

            controller
                .WithCallTo(c => c.Edit(post))
                .ShouldRenderDefaultView()
                .WithModel<PostInputModel>()
                .AndModelError("")
                .Containing("You have previously published a post with this title.");
        }
    }
}