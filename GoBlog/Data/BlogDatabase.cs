using System.Data.Entity;
using GoBlog.Data.Entities;

namespace GoBlog.Data
{
    public class BlogDatabase : DbContext, IRepository
    {
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Tag> Tags { get; set; }

        public BlogDatabase()
            : base("GoBlog")
        {
        }
    }
}