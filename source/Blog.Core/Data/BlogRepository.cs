using System.Data.Entity.Infrastructure;
using System.Linq;
using Blog.Core.Data.Entities;

namespace Blog.Core.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDatabase _blogDatabase = new BlogDatabase();

        public IQueryable<BlogEntry> All()
        {
            return _blogDatabase.BlogEntries.AsQueryable();
        }

        public BlogEntry Find(params object[] keyValues)
        {
            return _blogDatabase.BlogEntries.Find(keyValues);
        }

        public bool Add(BlogEntry entry)
        {
            _blogDatabase.BlogEntries.Add(entry);
            try
            {
                int entriesAdded = _blogDatabase.SaveChanges();
                return entriesAdded == 1;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            
            // if the command fails for another reason (e.g. 
            // database cannot be found) then  it makes sense 
            // to let the exception throw and the web server 
            // display a 500. right?

        }

        public void Delete(string headerSlug)
        {
            var entry = _blogDatabase.BlogEntries.Find(headerSlug);
            if (entry != null)
            {
                _blogDatabase.BlogEntries.Remove(entry);
                _blogDatabase.SaveChanges();
            }
        }

        public void Dispose()
        {
            _blogDatabase.Dispose();
        }

        public void Update(BlogEntry updatedEntry)
        {
            var original = _blogDatabase.BlogEntries.Find(updatedEntry.HeaderSlug);
            if (original != null)
            {
                original.HeaderSlug = updatedEntry.HeaderSlug;
                original.Header = updatedEntry.Header;
                original.Content = updatedEntry.Content;

                _blogDatabase.SaveChanges();
            }
        }

        public void IncrementView(BlogEntry updatedEntry)
        {
            updatedEntry.Views ++;
            Update(updatedEntry);
        }
    }
}