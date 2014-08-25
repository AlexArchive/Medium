using GoBlog.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.UnitTest.Support
{
    public class PostsMother
    {
        public static IEnumerable<Post> CreateEmptyPosts(int howMany = 2)
        {
            var post = new Post();
            return Enumerable.Repeat(post, howMany);
        }

        public static Post CreatePost(string withSlug = null)
        {
            var post = new Post { Slug = withSlug };
            return post;
        }
    }
}