using FakeDbSet;
using GoBlog.Infrastructure.Persistence;
using GoBlog.Infrastructure.Persistence.Entities;
using Moq;

namespace GoBlog.Test.Helpers
{
    internal static class RepositoryMockHelper
    {
        internal static Mock<IRepository> MockRepository()
        {
            var repository = new Mock<IRepository>();

            var posts = new InMemoryDbSet<Post>()
            {
                new Post {
                    Slug = "dynamic-contagion-part-one",
                    Title = "Dynamic contagion, part one",
                    Summary = "Suppose you're an epidemiologis",
                    Content = "Suppose you're an epidemiologis"
                },
                new Post {
                    Slug = "dynamic-contagion-part-two",
                    Title = "Dynamic contagion, part two",
                    Summary = "Last time I discussed how ",
                    Content = "Last time I discussed how"
                }
            };

            repository.Setup(db => db.Posts).Returns(posts);

            return repository;
        }
    }
}