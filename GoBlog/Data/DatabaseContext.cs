using System.Data.Entity;
using GoBlog.Data.Entities;

namespace GoBlog.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("GoBlog")
        {
        }

        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Tag> Tags { get; set; }
    }
}