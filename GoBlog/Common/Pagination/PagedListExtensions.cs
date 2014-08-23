using System.Collections.Generic;

namespace GoBlog.Common.Pagination
{
    public static class PagedListExtensions
    {
        public static PagedList<T> Paginate<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            return new PagedList<T>(source, pageNumber, pageSize);
        }
    }
}