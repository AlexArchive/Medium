using System.Linq;
using Blog.Core.Data.Entities;

namespace Blog.Core.Data
{
    public interface IBlogRepository
    {
        IQueryable<BlogEntry> All();
        BlogEntry Find(params object[] keyValues);
    }
}