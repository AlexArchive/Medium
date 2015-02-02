using System;
using System.Collections.Generic;

namespace Medium.DomainModel
{
    public class PostPage : List<PostModel>
    {
        public int PageNumber { get; set; }
        public int TotalPageCount { get; private set; }

        public bool HasNextPage
        {
            get { return PageNumber < TotalPageCount; }
        }

        public bool HasPreviousPage
        {
            get { return PageNumber > 1; }
        }

        public PostPage(IEnumerable<PostModel> posts, int pageNumber, int itemCount, int pageSize = 20)
        {
            AddRange(posts);
            PageNumber = pageNumber;
            TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize);
        }
    }
}