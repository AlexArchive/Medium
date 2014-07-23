using System.Data.Entity;
using System.Web.SessionState;
using GoBlog.Persistence.Entities;

namespace GoBlog.Persistence
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