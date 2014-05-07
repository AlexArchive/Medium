using System.Linq;

namespace Blog.Core.Paging
{
    public static class PagedListExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return new PagedList<T>(query, pageNumber, pageSize);
        }
    }
}