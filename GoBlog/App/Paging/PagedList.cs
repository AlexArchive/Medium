using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.Infrastructure.Paging
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> collection, int pageNumber, int pageSize)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (pageNumber < 0) throw new ArgumentOutOfRangeException("pageNumber");
            if (pageSize < 0) throw new ArgumentOutOfRangeException("pageSize");

            PageSize = pageSize;
            PageNumber = pageNumber;

            TotalCount = collection.Count();

            // If there are no elements in the collection; there is no
            // need to query the data source. 
            if (TotalCount == 0) return;

            PageCount = (int)Math.Ceiling((double)TotalCount / PageSize);


            AddRange(
                collection.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }

        public int TotalCount { get; set; }

    }
}