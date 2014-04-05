using System.Data.Entity;
using Blog.Core.Data.Entities;

namespace Blog.Core.Data
{
    public class BlogDatabase : DbContext
    {
        public IDbSet<BlogEntry> BlogEntries { get; set; }
        public IDbSet<Tag> Tags { get; set; }

        public BlogDatabase()
            : base("Blog")
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}