using GoBlog.Areas.Admin.Models;
using GoBlog.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.UnitTest.Support
{
    public class PostMother
    {
        public static Post CreatePost(string withSlug = "", string withTitle = "", string withContent = "")
        {
            var post = new Post
            {
                Slug = withSlug,
                Title = withTitle,
                Content = withContent
            };

            return post;
        }

        public static IEnumerable<Post> CreatePosts(int howMany = 2)
        {
            var post = CreatePost();
            return Enumerable.Repeat(post, howMany);
        }
    }

    public class PostInputMother
    {
        public static PostInputModel CreatePost()
        {
            return new PostInputModel();
        } 
    }

}