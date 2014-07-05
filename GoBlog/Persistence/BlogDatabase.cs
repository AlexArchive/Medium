using System.Data.Entity;
using GoBlog.Persistence.Entities;

namespace GoBlog.Persistence
{
    public class BlogDatabase : DbContext, IRepository
    {
        public IDbSet<Post> Posts { get; set; }

        public BlogDatabase()
            : base("GoBlog")
        {
        }
    }
}