using System.Collections.Generic;
using GoBlog.Domain.Model;

namespace GoBlog.Domain
{
    public interface IPostsRepository
    {
        IEnumerable<Post> AllPosts();
        Post FindPost(string slug);
        bool RemovePost(string slug);
        void AddPost(Post post);
        void UpdatePost(Post post);
    }
}