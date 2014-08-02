using GoBlog.Data.Entities;
using System.Collections.Generic;

namespace GoBlog.Data
{
    public interface IPostsRepository
    {
        IEnumerable<Post> All();
        Post Find(string slug);
    }
}