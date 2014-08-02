using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GoBlog.Data.Entities;

namespace GoBlog.Data
{
    public class PostsRepository : IPostsRepository
    {
        private readonly DatabaseContext database = new DatabaseContext();

        public IEnumerable<Post> All()
        {
            return database.Posts;
        }

        public Post Find(string slug)
        {
            return database.Posts.Find(slug);
        }
    }
}