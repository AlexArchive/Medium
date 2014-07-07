using System.Data.Entity;
using GoBlog.Persistence.Entities;

namespace GoBlog.Persistence
{
    // this is a pragmatic implementation, trust me.
    public interface IRepository
    {
        IDbSet<Post> Posts { get; }
        int SaveChanges();
    }
}