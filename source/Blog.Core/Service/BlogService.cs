using System.Data.Entity;
using System.Linq;
using Blog.Core.Data;
using Blog.Core.Data.Entities;
using Blog.Core.Paging;

namespace Blog.Core.Service
{
    public class BlogService
    {
        private readonly IBlogRepository _repository = new BlogRepository();

        public PagedList<BlogEntry> GetBlogEntries(int pageNumber, int pageSize)
        {
            var something =
                _repository
                    .All()
                    .Include(entry => entry.Tags)
                    .Where(entry => entry.Published)
                    .OrderByDescending(entry => entry.PublishDate);

            return something.ToPagedList(pageNumber, pageSize);
        }

        public BlogEntry GetBlogEntry(string headerSlug)
        {
            return _repository.Find(headerSlug);
        }
    }
}
