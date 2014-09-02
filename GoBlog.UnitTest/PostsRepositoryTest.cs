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
        public void All_ReturnsAllPosts()
        {
            const int Count = 2;
            context.Posts.AddRange(PostMother.CreatePosts(Count));

            var actual = repository.All();

            Assert.AreEqual(Count, actual.Count());
        }

        [Test]
        public void All_ReturnsAllPostsOrderedByPublishDate()
        {
            context.Posts.AddRange(new List<Post>
            {
                new Post { PublishDate = new DateTime(2014, 2, 1) },
                new Post { PublishDate = new DateTime(2014, 1, 1) },
            });

            var actual = repository.All();
            var expected = context.Posts.OrderBy(post => post.PublishDate);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void All_ReturnsAllPostsExcludingDrafts()
        {
            const int Count = 1;
            context.Posts.AddRange(PostMother.CreateDrafts(howMany: Count));

            var actual = repository.All();
         
            Assert.AreEqual(Count - Count, actual.Count());
        }

        [Test]
        public void All_EmptyDataSet_ReturnsEmptySequence()
        {
            var actual = repository.All();

            Assert.IsEmpty(actual);
        }

        [Test]
        public void Find_PostFound_ReturnsPost()
        {
            const string Slug = "abc";
            context.Posts.Add(PostMother.CreatePost(Slug));

            var actual = repository.Find(Slug);

            Assert.AreEqual(Slug, actual.Slug);
        }

        [Test]
        public void Find_PostNotFound_ReturnsNull()
        {
            var actual = repository.Find("abc");

            Assert.Null(actual);
        }

        [Test]
        public void Find_MoreThanOnePostFound_Throws()
        {
            const string Slug = "abc";
            context.Posts.AddRange(new List<Post>
            {
                PostMother.CreatePost(Slug),
                PostMother.CreatePost(Slug)
            });

            Assert.Throws<InvalidOperationException>(() => repository.Find(Slug));
        }

        [Test]
        public void Delete_RemovesPost()
        {
            const string Slug = "abc";
            context.Posts.Add(PostMother.CreatePost(Slug));

            repository.Delete(Slug);

            Assert.AreEqual(0, context.Posts.Count());
        }

        [Test]
        public void Delete_ExistentPost_ReturnsTrue()
        {
            const string Slug = "abc";
            context.Posts.Add(PostMother.CreatePost(Slug));

            var actual = repository.Delete(Slug);

            Assert.True(actual);
        }

        [Test]
        public void Delete_ExistentPost_CallsSaveChanges()
        {
            const string Slug = "abc";
            context.Posts.Add(PostMother.CreatePost(Slug));

            repository.Delete(Slug);

            Assert.AreEqual(1, context.SaveChangesCount);
        }
        
        [Test]
        public void Delete_NonExistentPost_ReturnsFalse()
        {
            var actual = repository.Delete("abc");

            Assert.False(actual);
        }

        [Test]
        public void Delete_NonExistentPost_DoesNotThrow()
        {
            repository.Delete("abc");
        }

        [Test]
        public void Add_AddsPost()
        {
            var post = PostMother.CreatePost();

            repository.Add(post);

            Assert.AreEqual(1, context.Posts.Count());
        }

        [Test]
        public void Add_CallsSaveChanges()
        {
            var post = PostMother.CreatePost();

            repository.Add(post);
                
            Assert.AreEqual(1, context.SaveChangesCount);
        }

        [Test]
        public void Add_AssignsSlugToPost()
        {
            var post = PostMother.CreatePost(withTitle: "arbitrary title");
            repository.Add(post);

            var actual = context.Posts.First();

            Assert.AreEqual(SlugConverter.Convert(post.Title), actual.Slug);
        }

        [Test]
        public void Add_AssignsSummaryToPost()
        {
            var post = PostMother.CreatePost(withContent: "arbitrary content");
            repository.Add(post);

            var actual = context.Posts.First();

            Assert.AreEqual(SummaryConverter.Convert(post.Content), actual.Summary);
        }

        [Test]
        public void Add_AssignPublishDateToPost()
        {
            var post = PostMother.CreatePost();
            repository.Add(post);

            var actual = context.Posts.First();

            Assert.AreNotEqual(DateTime.MinValue, actual.PublishDate);
        }
    }
}