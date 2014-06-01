using System.Data.Entity;
using GoBlog.Domain.Infrastructure.Persistence.Entities;

namespace GoBlog.Domain.Infrastructure.Persistence
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