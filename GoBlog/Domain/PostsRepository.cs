using GoBlog.Domain.Model;
using System;
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

        public IEnumerable<Post> AllPosts()
        {
            return context
                .Posts
                .Where(post => !post.Draft)
                .OrderBy(post => post.PublishDate);
        }

        public Post FindPost(string slug)
        {
            return context.Posts.Find(slug);
        }

        public bool RemovePost(string slug)
        {
            var post = FindPost(slug);
            if (post == null) return false;

            context.Posts.Remove(post);
            context.SaveChanges();
            return true;
        }

        public void AddPost(Post post)
        {
            post.Slug = SlugConverter.Convert(post.Title);
            post.Summary = SummaryConverter.Convert(post.Content);
            post.PublishDate = DateTime.Now;

            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            var original = FindPost(post.Slug);

            if (original == null)
            {
                throw new Exception();
            }

            context.Posts.Remove(original);
            context.SaveChanges();

            post.Slug = SlugConverter.Convert(post.Title);
            post.Summary = SummaryConverter.Convert(post.Content);
            post.PublishDate = original.PublishDate;

            if (FindPost(post.Slug) != null)
            {
                throw new Exception();
            }

            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}