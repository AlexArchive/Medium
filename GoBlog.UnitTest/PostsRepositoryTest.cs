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
        private DatabaseContextDouble contextDouble;

        [SetUp]
        public void SetUp()
        {
            contextDouble = new DatabaseContextDouble();
            repository = new PostsRepository(contextDouble);
        }

        [Test]
        public void All_ReturnsAllPosts()
        {
            const int Count = 2;
            contextDouble.Posts.AddRange(PostsMother.CreateEmptyPosts(Count));

            var actual = repository.All();

            Assert.AreEqual(Count, actual.Count());
        }

        [Test]
        public void All_ReturnsAllPostsOrderedByPublishDate()
        {
            contextDouble.Posts.AddRange(new List<Post>
            {
                new Post { PublishDate = new DateTime(2014, 2, 1) },
                new Post { PublishDate = new DateTime(2014, 1, 1) },
            });

            var actual = repository.All();
            var expected = contextDouble.Posts.OrderBy(post => post.PublishDate);

            Assert.AreEqual(expected, actual);
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
            contextDouble.Posts.Add(PostsMother.CreatePost(Slug));

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
            contextDouble.Posts.AddRange(new List<Post>
            {
                PostsMother.CreatePost(Slug),
                PostsMother.CreatePost(Slug)
            });

            Assert.Throws<InvalidOperationException>(() => repository.Find(Slug));
        }

        [Test]
        public void Delete_RemovesPost()
        {
            const string Slug = "abc";
            contextDouble.Posts.Add(PostsMother.CreatePost(Slug));

            repository.Delete(Slug);

            Assert.AreEqual(0, contextDouble.Posts.Count());
        }

        [Test]
        public void Delete_ExistentPost_ReturnsTrue()
        {
            const string Slug = "abc";
            contextDouble.Posts.Add(PostsMother.CreatePost(Slug));

            var actual = repository.Delete(Slug);

            Assert.True(actual);
        }

        [Test]
        public void Delete_ExistentPost_CallsSaveChanges()
        {
            const string Slug = "abc";
            contextDouble.Posts.Add(PostsMother.CreatePost(Slug));

            repository.Delete(Slug);

            Assert.AreEqual(1, contextDouble.SaveChangesCount);
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
    }
}