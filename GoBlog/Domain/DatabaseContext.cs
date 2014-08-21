using System.Data.Entity;
using GoBlog.Domain.Model;

namespace GoBlog.Domain
{
    public class DatabaseContext : DbContext
    {
        public virtual IDbSet<Post> Posts { get; set; }
    }
}