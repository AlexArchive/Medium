using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GoBlog.Data.Entities;

namespace GoBlog.Data
{
    // This is a pragmatic implementation. Trust me.
    public interface IRepository
    {
        IDbSet<Post> Posts { get; }
        IDbSet<Tag> Tags { get; }
        int SaveChanges();
    }
}