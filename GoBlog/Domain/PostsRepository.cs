using System;
using GoBlog.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.Domain
{
    public class PostsRepository : IPostsRepository
    {
        private readonly IDatabaseContext context;

        public PostsRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Post> All()
        {
            return context
                .Posts
                .Where(post => !post.Draft)
                .OrderBy(post => post.PublishDate);
        }

        public Post Find(string slug)
        {
            return context
                .Posts
                .SingleOrDefault(post => post.Slug == slug);
        }

        public bool Delete(string slug)
        {
            var post = Find(slug);
            if (post == null) return false;

            context.Posts.Remove(post);
            context.SaveChanges();
            return true;
        }

        public void Add(Post post)
        {
            post.Slug = SlugConverter.Convert(post.Title);
            post.Summary = SummaryConverter.Convert(post.Content);
            post.PublishDate = DateTime.Now;

            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}