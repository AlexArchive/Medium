using System;
using System.Collections.Generic;
using System.Linq;

namespace Medium.DomainModel
{
    public class PostPage : List<PostModel>
    {
        public int PageNumber { get; private set; }
        public int TotalPageCount { get; private set; }

        public bool HasNextPage
        {
            get { return PageNumber < TotalPageCount; }
        }

        public bool HasPreviousPage
        {
            get {  return PageNumber > 1; }
        }

        public PostPage(
            IEnumerable<PostModel> source, 
            int pageNumber, 
            int pageSize)
        {
            PageNumber = pageNumber;
            TotalPageCount = (int) Math.Ceiling(source.Count() / (double) pageSize);

            var part = source.Skip((PageNumber - 1) * pageSize).Take(pageSize);
            AddRange(part);
        }
    }

    public static class PostPageExtension
    {
        public static PostPage ToPostPage(
            this IEnumerable<PostModel> source,
            int pageNumber,
            int pageSize)
        {
            return new PostPage(source, pageNumber, pageSize);
        }
    }
}