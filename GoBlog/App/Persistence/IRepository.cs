using System.Data.Entity;
using GoBlog.Infrastructure.Persistence.Entities;

namespace GoBlog.Infrastructure.Persistence
{
    public interface IRepository
    {
        IDbSet<Post> Posts { get; }
    }
}