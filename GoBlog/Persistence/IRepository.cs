using System.Data.Entity;
using GoBlog.Persistence.Entities;

namespace GoBlog.Persistence
{
    // This is a pragmatic implementation. Trust me.
    public interface IRepository
    {
        IDbSet<Post> Posts { get; }
        int SaveChanges();
    }
}