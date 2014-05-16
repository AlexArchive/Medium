using Blog.Core.Infrastructure.Persistence;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Service
{
    public class EntryService
    {
        public PagedList<BlogEntry> PaginatedList(int pageNumber, int pageSize)
        {
            using (var repository = new Repository<BlogEntry>())
            {
                var entries =
                    repository
                        .All()
                        .OrderByDescending(e => e.PublishedAt);

                return entries.ToPagedList(pageNumber, pageSize);
            }
        }

        public int EntriesCount()
        {
            using (var repository = new Repository<BlogEntry>())
            {
                return repository.All().Count();
            }
        }

        public List<BlogEntry> List()
        {
            using (var repository = new Repository<BlogEntry>())
            {
                return
                    repository
                        .All()
                        .OrderByDescending(e => e.PublishedAt).ToList();
            }
        }

        public BlogEntry Single(string slug)
        {
            using (var repository = new Repository<BlogEntry>())
            {
                var entry =
                    repository
                        .All()
                        .FirstOrDefault(e => e.Slug == slug);

                return entry;
            }
        }

        public bool Add(BlogEntry entry)
        {
            try
            {
                using (var repository = new Repository<BlogEntry>())
                {
                    repository.Add(entry);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
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
                repository.Update(entry, entry.Slug);
            }
        }

        //private static BlogEntry MakeBlogEntry(string header, string headerSlug, string content, bool published)
        //{
        //    var entry = new BlogEntry
        //    {
        //        Header = header,
        //        HeaderSlug = headerSlug,
        //        Content = content,
        //        Published = published,
        //        PublishDate = DateTime.Now,
        //        Summary = MakeSummary(content),
        //        Views = 0
        //    };
        //    return entry;
        //}


    }
}