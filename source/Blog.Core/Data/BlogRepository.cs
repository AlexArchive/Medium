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
            catch
            {
                return false;
            }
               
        }

        public void Dispose()
        {
            _blogDatabase.Dispose();
        }
    }
}