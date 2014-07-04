using System.Data.Entity;
using GoBlog.Infrastructure.Persistence.Entities;

namespace GoBlog.Infrastructure.Persistence.Migrations
{
    public class DatabaseSeeder : DropCreateDatabaseAlways<BlogDatabase>
    {
        protected override void Seed(BlogDatabase context)
        {
            context.Posts.Add(new Post { Slug = "hello", Title = "Hello", Summary = "Hello", Content = "Hello" });
            context.SaveChanges();
        }
    }
}