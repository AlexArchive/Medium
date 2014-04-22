using System;
using System.Linq;
using System.Collections.Generic;

namespace Blog.Core.Paging
{
    public class PagedList<T> : List<T>
    {
        /// <summary>
        /// The number of elements in this page.
        /// </summary>
        /// <remarks>
        /// Generally, this number should be the same for all pages.
        /// </remarks>
        public int PageSize { get; private set; }

        /// <summary>
        /// The sequential index of this page (for example, page 1 of 5).
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// The total number of list items in all pages.
        /// </summary>
        public int TotalItemCount { get; private set; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int TotalPageCount { get; private set; }

        protected PagedList()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="query">The collection of items to paginate.</param>
        /// <param name="pageNumber">The sequential index of this page.</param>
        /// <param name="pageSize">The number of elements in this page.</param>
        public PagedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;

            TotalItemCount = query.Count();

            // If there are no elements in the collection; there is no
            // need to query the data source. 
            if (TotalItemCount == 0) return;

            TotalPageCount = (int) Math.Ceiling((double) TotalItemCount / PageSize);

            AddRange(
                query.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }
    }
}