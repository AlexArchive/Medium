using System.Collections.Generic;
using System.Linq;
using GoBlog.Domain.Infrastructure.Persistence;
using GoBlog.Domain.Infrastructure.Persistence.Entities;

namespace GoBlog.Domain.Service
{
    public class TagsService
    {
        public IEnumerable<string> Search(string term)
        {
            using (var repository = new Repository<Tag>())
            {
                return repository.Find(t => t.Name.ToLower().Contains(term.ToLower())).Select(t => t.Name);
            }
        }
    }
}