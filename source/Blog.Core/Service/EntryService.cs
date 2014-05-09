using Blog.Core.Infrastructure.Persistence;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Core.Paging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Blog.Core.Service
{
    public class EntryService
    {
        public PagedList<BlogEntry> ListPaginated(int pageNumber, int pageSize)
        {
            using (var repository = new Repository<BlogEntry>())
            {
                var entries =
                    repository
                        .All()
                        .OrderByDescending(e => e.PublishDate);

                return entries.ToPagedList(pageNumber, pageSize);
            }
        }

        public List<BlogEntry> List()
        {
            using (var repository = new Repository<BlogEntry>())
            {
                return
                    repository
                        .All()
                        .OrderByDescending(e => e.PublishDate).ToList();
            }
        }

        public BlogEntry Get(string slug)
        {
            using (var repository = new Repository<BlogEntry>())
            {
                var entry =
                    repository
                        .All()
                        .FirstOrDefault(e => e.HeaderSlug == slug);

                return entry;
            }
        }

        public bool Add(string header, string headerSlug, string content)
        {
            var entry = MakeBlogEntry(header, headerSlug, content);

            using (var repository = new Repository<BlogEntry>())
            {
                try
                {
                    repository.Add(entry);
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }

        public void Delete(string slug)
        {
            using (var repository = new Repository<BlogEntry>())
            {
                var entry = repository.Find(slug);
                repository.Delete(entry);
            }
        }

        public void Update(BlogEntry entry)
        {
            using (var repository = new Repository<BlogEntry>())
            {
                repository.UpdatePartial(entry, e => e.HeaderSlug, e => e.Header, e => e.Content);
            }
        }

        private static BlogEntry MakeBlogEntry(string header, string headerSlug, string content)
        {
            var entry = new BlogEntry
            {
                Header = header,
                HeaderSlug = headerSlug,
                Content = content,
                PublishDate = DateTime.Now,
                Summary = MakeSummary(content),
                Views = 0
            };
            return entry;
        }

        private static string MakeSummary(string content)
        {
            return content.Length < 750 ? content : content.Substring(0, 750) + "...";
        }
    }
}