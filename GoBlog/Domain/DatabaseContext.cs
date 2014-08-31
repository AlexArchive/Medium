using System.Data.Entity;
using GoBlog.Domain.Model;

namespace GoBlog.Domain
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public virtual DbSet<Post> Posts { get; set; }
    }
}