using EntityFramework.Testing.Moq;
using GoBlog.Domain;
using GoBlog.Domain.Model;
using GoBlog.UnitTest.Support;
using Moq;
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
        private MockDbSet<Post> databaseSet;
        private Mock<DatabaseContext> databaseContext;
        
        [SetUp]
        public void SetUp()
        {
            databaseSet = new MockDbSet<Post>();
            databaseSet.SetupLinq();

            databaseContext = new Mock<DatabaseContext>();
            databaseContext.Setup(context => context.Posts).Returns(databaseSet.Object);

            repository = new PostsRepository(databaseContext.Object);
        }

        [Test]
        public void All_ReturnsAllPosts()
        {
            const int Count = 2;
            databaseSet.SetupSeedData(PostsMother.CreateEmptyPosts(Count));

            var actual = repository.All();

            Assert.AreEqual(Count, actual.Count());
        }

        [Test]
        public void All_ReturnsAllPostsOrderedByPublishDate()
        {
            databaseSet.SetupSeedData(new List<Post>
            {
                new Post { PublishDate = new DateTime(2014, 2, 1) },
                new Post { PublishDate = new DateTime(2014, 1, 1) },
            });

            var actual = repository.All();
            var expected = databaseSet.Data.OrderBy(post => post.PublishDate);

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
            databaseSet.SetupSeedData(new List<Post>
            {
                PostsMother.CreatePost(Slug)
            });

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
            databaseSet.SetupSeedData(new List<Post>
            {
                PostsMother.CreatePost(Slug),
                PostsMother.CreatePost(Slug)
            });

            Assert.Throws<InvalidOperationException>(() => repository.Find(Slug));
        }
    }
}