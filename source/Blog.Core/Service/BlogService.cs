using System;
using System.Data.Entity;
using System.Linq;
using Blog.Core.Data;
using Blog.Core.Data.Entities;
using Blog.Core.Paging;
using MarkdownSharp;

namespace Blog.Core.Service
{
    public class BlogService
    {
        private readonly Markdown _markdown = new Markdown();

        public PagedList<BlogEntry> GetBlogEntries(int pageNumber, int pageSize)
        {
            using (var repository = new BlogRepository())
            {
                var something =
                    repository
                        .All()
                        .Include(entry => entry.Tags)
                        .Where(entry => entry.Published)
                        .OrderByDescending(entry => entry.PublishDate);

                return something.ToPagedList(pageNumber, pageSize);
            }
        }

        public BlogEntry Get(string headerSlug)
        {
            using (var repository = new BlogRepository())
                return repository.Find(headerSlug);
        }

        public bool AddBlogEntry(string header, string headerSlug, string content)
        {
            BlogEntry entry = new BlogEntry
            {
                Header = header,
                HeaderSlug = headerSlug,
                Content = content,
                PublishDate = DateTime.Now,
                Published = true,
                Summary = content.Length < 750 ? content : content.Substring(0, 750),
                Views = 0
            };
            using (var repository = new BlogRepository())
            {
                var success = repository.Add(entry);
             return success;
            }
        }
    }
}
