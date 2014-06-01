using System.Data.Entity;
using Blog.Domain.Infrastructure.Persistence.Entities;

namespace Blog.Domain.Infrastructure.Persistence
{
    public class BlogDatabase : DbContext
    {
        public DbSet<Entry> BlogEntries { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public BlogDatabase()
            : base("NiekGay")
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}