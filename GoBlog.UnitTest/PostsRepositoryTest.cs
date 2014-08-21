using EntityFramework.Testing.Moq;
using GoBlog.Domain;
using GoBlog.Domain.Model;
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
            databaseSet.SetupSeedData(Enumerable.Repeat(new Post(), 2));

            var actual = repository.All();

            Assert.AreEqual(2, actual.Count());
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
    }
}