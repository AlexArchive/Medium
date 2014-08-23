using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.Common.Pagination
{
    public sealed class PagedList<T> : List<T>
    {
        public int PageNumber { get; private set; }
        public int TotalPageCount { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool HasPreviousPage { get; private set; }
        
        public PagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentOutOfRangeException();

            PageNumber = pageNumber;
            TotalPageCount = (int) Math.Ceiling(source.Count() / (double) pageSize);
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < TotalPageCount;

            var partition = source.Skip((PageNumber - 1) * pageSize).Take(pageSize);
            AddRange(partition);
        }
    }
}