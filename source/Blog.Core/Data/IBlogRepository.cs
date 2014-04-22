using System;
using System.Linq;
using Blog.Core.Data.Entities;

namespace Blog.Core.Data
{
    public interface IBlogRepository : IDisposable
    {
        IQueryable<BlogEntry> All();
        BlogEntry Find(params object[] keyValues);
        bool Add(BlogEntry entry);
    }
}