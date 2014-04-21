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
        private readonly IBlogRepository _repository = new BlogRepository();
        private readonly Markdown _markdown = new Markdown();

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
            var a = _repository.Find(headerSlug);

            return new BlogEntry
            {
                Content = _markdown.Transform(a.Content),
                HeaderSlug = a.HeaderSlug,
                Header = a.Header,
                PublishDate = a.PublishDate,
                Published = a.Published,
                Summary = a.Summary,
                Tags = a.Tags,
                Views = a.Views
            };
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

            var success = _repository.Add(entry);
            return success;
        }
    }
}
