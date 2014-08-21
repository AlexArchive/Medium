using System.Linq;
using GoBlog.Domain.Model;
using System.Collections.Generic;

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
            return context.Posts
                          .OrderBy(post => post.PublishDate);
        } 
    }
}