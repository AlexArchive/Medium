using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Blog.Core.Paging
{
    public class PagedList<T> : IEnumerable<T>
    {
        private readonly List<T> _pagedList = new List<T>();

        public int PageSize { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalItemCount { get; private set; }
        public int TotalPageCount { get; private set; }

        public PagedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;

            TotalItemCount = query.Count();

            // maybe this will yield a small performance benefit because we won't waste time with the pagination logic.
            //if (TotalItemCount == 0) return;

            TotalPageCount = (int) Math.Ceiling((double) TotalItemCount / PageSize);

            var pagedList = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            _pagedList.AddRange(pagedList.ToList());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _pagedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _pagedList.GetEnumerator();
        }
    }
}