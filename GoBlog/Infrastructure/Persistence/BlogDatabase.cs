using System.Data.Entity;
using GoBlog.Infrastructure.Persistence.Entities;

namespace GoBlog.Infrastructure.Persistence
{
    public class BlogDatabase : DbContext, IRepository
    {
        public IDbSet<Post> Posts { get; set; }

        public BlogDatabase() : base ("GoBlog")
        {
        }
    }
}