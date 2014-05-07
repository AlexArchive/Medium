using System;
using System.Linq;
using System.Collections.Generic;

namespace Blog.Core.Paging
{
    public class PagedList<T> : List<T>
    {
        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        public int TotalItemCount { get; private set; }

        public int TotalPageCount { get; private set; }

        protected PagedList()
        {
        }
        
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