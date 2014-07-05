using System.Collections.Generic;

namespace GoBlog.Infrastructure.Paging
{
    public static class PagedListExtensions
    {
        public static PagedList<T> ToPagedList<T>(
            this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            return new PagedList<T>(source, pageNumber, pageSize);
        }
    }
}