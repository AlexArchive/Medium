using System.Linq;

namespace Blog.Core.Paging
{
    /// <summary>
    /// Defines extension methods on the <see cref="PagedList{T}"/> class.
    /// </summary>
    public static class PagedListExtensions
    {
        /// <summary>
        /// Initializes an instance of the <see cref="PagedList{T}"/> class based on the given arguments.
        /// </summary>
        /// <typeparam name="T">The type of the collection to paginate.</typeparam>
        /// <param name="query">The collection of items to paginate.</param>
        /// <param name="pageNumber">The sequential index of this page.</param>
        /// <param name="pageSize">The number of elements in this page.</param>
        /// <returns>An instance of the <see cref="PagedList{T}"/> class.</returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return new PagedList<T>(query, pageNumber, pageSize);
        }
    }
}