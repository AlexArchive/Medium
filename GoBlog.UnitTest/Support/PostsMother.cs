using GoBlog.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.UnitTest.Support
{
    public class PostsMother
    {
        public static IEnumerable<Post> CreatePosts()
        {
            var post = new Post();
            return Enumerable.Repeat(post, 6);
        }
    }
}