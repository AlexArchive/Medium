using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.Infrastructure.Paging
{
    public class PagedList<T> : List<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }

        public PagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (pageNumber < 0) throw new ArgumentOutOfRangeException("pageNumber");
            if (pageSize < 0)  throw new ArgumentOutOfRangeException("pageSize");
 
            PageSize = pageSize;
            PageNumber = pageNumber;

            TotalCount = source.Count();

            // If there are no elements in the sequence, there is no need no enumerate 
            // the sequence again.
            if (TotalCount == 0) return;

            PageCount = (int) Math.Ceiling((double) TotalCount / PageSize);

            AddRange(source.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }
    }
}