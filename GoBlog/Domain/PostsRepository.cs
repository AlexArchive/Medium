using GoBlog.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.Domain
{
    public class PostsRepository : IPostsRepository
    {
        private readonly DatabaseContext context;

        public PostsRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Post> All()
        {
            return context
                .Posts
                .OrderBy(post => post.PublishDate);
        }

        public Post Find(string slug)
        {
            return context
                .Posts
                .SingleOrDefault(post => post.Slug == slug);
        }
   }
}