using System.Data.Entity;
using Blog.Core.Infrastructure.Persistence.Entities;

namespace Blog.Core.Infrastructure.Persistence
{
    public class BlogDatabase : DbContext
    {
        public DbSet<BlogEntry> BlogEntries { get; set; }

        public BlogDatabase()
            : base("BlogProductionDatabase1")
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}