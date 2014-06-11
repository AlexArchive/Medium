using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GoBlog.Domain.Infrastructure.Persistence;
using GoBlog.Domain.Infrastructure.Persistence.Entities;
using GoBlog.Domain.Paging;

namespace GoBlog.Domain.Service
{
    public class EntryService
    {
        public PagedList<Entry> PaginatedList(int pageNumber, int pageSize)
        {
            using (var repository = new Repository<Entry>())
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
            using (var repository = new Repository<Entry>())
            {
                return repository.All().Count();
            }
        }

        public List<Entry> List()
        {
            using (var repository = new Repository<Entry>())
            {
                return
                    repository
                        .All()
                        .OrderByDescending(e => e.PublishedAt).ToList();
            }
        }

        public Entry Single(string slug)
        {
            using (var repository = new Repository<Entry>())
            {
                var entry =
                    repository
                        .All()
                        .Include(x => x.Tags)
                        .FirstOrDefault(e => e.Slug == slug);

                return entry;
            }
        }

        public bool Add(Entry entry)
        {
            entry.CreatedAt = DateTime.Now;
            entry.PublishedAt = DateTime.Now;
            entry.Summary = "coming soon.";

            try
            {
                var context = new BlogDatabase();
                using (var tagRepo = new Repository<Tag>(context))
                using (var entryRepo = new Repository<Entry>(context))
                {
                    foreach (var tag in entry.Tags)
                        tagRepo.Attach(tag);

                    entryRepo.Add(entry);
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
            using (var repository = new Repository<Entry>())
            {
                var entry = repository.Find(slug);
                repository.Delete(entry);
            }
        }

        public void Update(Entry entry)
        {
            using (var repository = new Repository<Entry>())
            {
                var existing = repository.All().Include(x => x.Tags).SingleOrDefault(x => x.Slug == entry.Slug);
                repository.Update(existing,entry);
            }
        }
    }
}