using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Core.Infrastructure.Persistence;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Core.Paging;

namespace Blog.Core.Service
{
    public class BlogService
    {
        public PagedList<BlogEntry> GetBlogEntries(int pageNumber, int pageSize)
        {
            using (var repository = new Repository<BlogEntry>(new BlogDatabase()))
            {
                var something =
                    repository
                        .All()
                        .OrderByDescending(entry => entry.PublishDate);

                return something.ToPagedList(pageNumber, pageSize);
            }
        }

        public List<BlogEntry> GetAll()
        {
            using (var repository = new Repository<BlogEntry>(new BlogDatabase()))
            {
                return repository.All()
                    .OrderByDescending(entry => entry.PublishDate).ToList();
            }

        }

        public BlogEntry Get(string headerSlug)
        {
            using (var repository = new Repository<BlogEntry>(new BlogDatabase()))
            {
                var entry = 
                    repository.All()
                        .FirstOrDefault(e => e.HeaderSlug == headerSlug);

                return entry;

            }
        }

        public bool AddBlogEntry(string header, string headerSlug, string content)
        {
            BlogEntry entry = new BlogEntry
            {
                Header = header,
                HeaderSlug = headerSlug,
                Content = content,
                PublishDate = DateTime.Now,
                Summary = MakeSummary(content),
                Views = 0
            };

            using (var repository = new Repository<BlogEntry>(new BlogDatabase()))
            {
                var success = repository.Add(entry);
                //return success;
                return true;
                //todo: handle exceptions here instead.
            }
        }

        private static string MakeSummary(string content)
        {
            return content.Length < 750 ? content : content.Substring(0, 750) + "...";
        }

        public void Delete(string headerSlug)
        {
            using (var repository = new Repository<BlogEntry>(new BlogDatabase()))
            {
                var entry = repository.Find(headerSlug);
                repository.Delete(entry);
            }
        }

        public void Update(BlogEntry entry)
        {
            using (var repository = new Repository<BlogEntry>(new BlogDatabase()))
            {
                repository.Update(entry, entry.HeaderSlug);
            }
        }


    }
}
