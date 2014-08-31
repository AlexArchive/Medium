using System.Data.Entity;
using GoBlog.Domain.Model;

namespace GoBlog.Domain
{
    public interface IDatabaseContext
    {
        DbSet<Post> Posts { get; }
        int SaveChanges();
    }
}