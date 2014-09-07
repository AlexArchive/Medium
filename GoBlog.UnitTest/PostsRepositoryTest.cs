using System.CodeDom;
using GoBlog.Domain;
using GoBlog.Domain.Model;
using GoBlog.UnitTest.Support;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.UnitTest
{
    [TestFixture]
    public class PostsRepositoryTest
    {
        private PostsRepository repository;
        private DatabaseContextDouble context;

        [SetUp]
        public void SetUp()
        {
            context = new DatabaseContextDouble();
            repository = new PostsRepository(context);
        }

        [Test]
        public void AllPosts_ReturnsAllPosts()
        {
            const int Count = 2;
            context.Posts.AddRange(PostMother.CreatePosts(Count));

            var actual = repository.AllPosts();

            Assert.AreEqual(Count, actual.Count());
        }

        [Test]
        public void AllPosts_ReturnsAllPostsOrderedByPublishDate()
        {
            context.Posts.AddRange(new List<Post>
            {
                new Post { PublishDate = new DateTime(2014, 2, 1) },
                new Post { PublishDate = new DateTime(2014, 1, 1) },
            });

            var actual = repository.AllPosts();
            var expected = context.Posts.OrderBy(post => post.PublishDate);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AllPosts_ReturnsAllPostsExcludingDrafts()
        {
            const int Count = 1;
            context.Posts.AddRange(PostMother.CreateDrafts(howMany: Count));

            var actual = repository.AllPosts();
         
            Assert.AreEqual(Count - Count, actual.Count());
        }

        [Test]
        public void AllPosts_EmptyDataSet_ReturnsEmptySequence()
        {
            var actual = repository.AllPosts();

            Assert.IsEmpty(actual);
        }

        [Test]
        public void FindPost_PostFound_ReturnsPost()
        {
            const string Slug = "abc";
            context.Posts.Add(PostMother.CreatePost(Slug));

            var actual = repository.FindPost(Slug);

            Assert.AreEqual(Slug, actual.Slug);
        }

        [Test]
        public void FindPost_PostNotFound_ReturnsNull()
        {
            var actual = repository.FindPost("abc");

            Assert.Null(actual);
        }

        [Test]
        public void FindPost_MoreThanOnePostFound_Throws()
        {
            const string Slug = "abc";
            context.Posts.AddRange(new List<Post>
            {
                PostMother.CreatePost(Slug),
                PostMother.CreatePost(Slug)
            });

            Assert.Throws<InvalidOperationException>(() => repository.FindPost(Slug));
        }

        [Test]
        public void RemovePost_RemovesPost()
        {
            const string Slug = "abc";
            context.Posts.Add(PostMother.CreatePost(Slug));

            repository.RemovePost(Slug);

            Assert.AreEqual(0, context.Posts.Count());
        }

        [Test]
        public void RemovePost_ExistentPost_ReturnsTrue()
        {
            const string Slug = "abc";
            context.Posts.Add(PostMother.CreatePost(Slug));

            var actual = repository.RemovePost(Slug);

            Assert.True(actual);
        }

        [Test]
        public void RemovePost_ExistentPost_CallsSaveChanges()
        {
            const string Slug = "abc";
            context.Posts.Add(PostMother.CreatePost(Slug));

            repository.RemovePost(Slug);

            Assert.AreEqual(1, context.SaveChangesCount);
        }
        
        [Test]
        public void RemovePost_NonExistentPost_ReturnsFalse()
        {
            var actual = repository.RemovePost("abc");

            Assert.False(actual);
        }

        [Test]
        public void RemovePost_NonExistentPost_DoesNotThrow()
        {
            repository.RemovePost("abc");
        }

        [Test]
        public void AddPost_AddsPost()
        {
            var post = PostMother.CreatePost();

            repository.AddPost(post);

            Assert.AreEqual(1, context.Posts.Count());
        }

        [Test]
        public void AddPost_CallsSaveChanges()
        {
            var post = PostMother.CreatePost();

            repository.AddPost(post);
                
            Assert.AreEqual(1, context.SaveChangesCount);
        }

        [Test]
        public void AddPost_AssignsSlugToPost()
        {
            var post = PostMother.CreatePost(withTitle: "arbitrary title");
            repository.AddPost(post);

            var actual = context.Posts.First();

            Assert.AreEqual(SlugConverter.Convert(post.Title), actual.Slug);
        }

        [Test]
        public void AddPost_AssignsSummaryToPost()
        {
            var post = PostMother.CreatePost(withContent: "arbitrary content");
            repository.AddPost(post);

            var actual = context.Posts.First();

            Assert.AreEqual(SummaryConverter.Convert(post.Content), actual.Summary);
        }

        [Test]
        public void AddPost_AssignPublishDateToPost()
        {
            var post = PostMother.CreatePost();
            repository.AddPost(post);

            var actual = context.Posts.First();

            Assert.AreNotEqual(DateTime.MinValue, actual.PublishDate);
        }

        [Test]
        public void UpdatePost_UpdatesPost()
        {
            var original = PostMother.CreatePost();
            var updated = PostMother.CreatePost();
            context.Posts.Add(original);

            updated.Draft = true;
            repository.UpdatePost(updated);

            var actual = context.Posts.First();
            Assert.AreEqual(true, actual.Draft);
        }

        [Test]
        public void UpdatePost_CallsSaveChanges()
        {
            var original = PostMother.CreatePost();
            var post = PostMother.CreatePost();
            context.Posts.Add(original);
            
            repository.UpdatePost(post);

            Assert.True(context.SaveChangesCount >= 1);
        }

        [Test]
        public void UpdatePost_AssignsSlugToPost()
        {
            var original = PostMother.CreatePost();
            var updated = PostMother.CreatePost();
            context.Posts.Add(original);

            updated.Title = "dumb";
            repository.UpdatePost(updated);

            var actual = context.Posts.First();
            Assert.AreEqual(
                SlugConverter.Convert(updated.Title), 
                actual.Slug);
        }

        [Test]
        public void UpdatePost_AssignsSummaryToPost()
        {
            var original = PostMother.CreatePost();
            var updated = PostMother.CreatePost();
            context.Posts.Add(original);

            updated.Content = "content";
            repository.UpdatePost(updated);

            var actual = context.Posts.First();
            Assert.AreEqual(
                SummaryConverter.Convert(updated.Content),
                actual.Summary);
        }

        [Test]
        public void UpdatePost_DoesNotAssignPublishDate()
        {
            var original = PostMother.CreatePost();
            var updated = PostMother.CreatePost();
            context.Posts.Add(original);
            var dateBeforeCallingUpdate = original.PublishDate;

            repository.UpdatePost(updated);

            var actual = context.Posts.First();
            Assert.AreEqual(dateBeforeCallingUpdate, actual.PublishDate);
        }

        [Test]
        public void UpdatePost_OccupiedSlug_Throws()
        {
            const string Title = "Title";
            var original = PostMother.CreatePost(SlugConverter.Convert(Title));
            context.Posts.Add(original);

            var updated = PostMother.CreatePost();
            updated.Title = Title;

            Assert.Throws<Exception>(() => repository.UpdatePost(updated));
        }
    }
}