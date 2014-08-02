using GoBlog.Areas.Admin.Models;
using GoBlog.AutoMapper;
using GoBlog.Common;
using GoBlog.Data;
using GoBlog.Data.Entities;
using GoBlog.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostsRepository<> repository;

        public PostsController(IPostsRepository<> repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            var posts =
                repository.Posts
                          .OrderBy(post => post.PublishedAt);

            return View(posts.MapTo<PostViewModel>());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PostInputModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var post = model.MapTo<Post>();
            post.PublishedAt = DateTime.Now;

            if (!UpdatePost(post))
            {
                return View(model);
            }

            TempData["newPost"] = true;
            return RedirectToAction("Edit", new { slug = post.Slug });
        }

        public ActionResult Edit(string slug)
        {
            var post = 
                repository.Posts
                          .Find(slug);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post.MapTo<PostInputModel>());
        }

        [HttpPost]
        public ActionResult Edit(PostInputModel model)
        {
            if (!ModelState.IsValid) return View("Edit", model);

            var post = 
                repository.Posts
                           .Find(model.Slug);

            if (post == null)
            {
                return HttpNotFound();
            }

            repository.Posts.Remove(post);
            repository.SaveChanges();

            model.MapPropertiesToInstance(post);

            if (!UpdatePost(post))
            {
                return View(model);
            }

            return Edit(post.Slug);
        }

        public ActionResult Delete(string slug)
        {
            var post = 
                repository.Posts
                          .Find(slug);

            if (post == null)
            {
                return HttpNotFound();
            }

            repository.Posts.Remove(post);
            repository.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool UpdatePost(Post post)
        {
            post.Slug = SlugConverter.Convert(post.Title);
            post.Summary = Summarize(post.Content);

            AttachTags(post);

            if (repository.Posts.Find(post.Slug) != null)
            {
                ModelState.AddModelError("", 
                    "You have previously published a post with this title. Please choose another one.");
                return false;
            }

            repository.Posts.Add(post);
            repository.SaveChanges();
            return true;
        }

        private static string Summarize(string content)
        {
            if (!content.Contains(Environment.NewLine))
            {
                return content;
            }

            return content.Split(new[] { Environment.NewLine }, StringSplitOptions.None).First();
        }

        private void AttachTags(Post post)
        {
            foreach (var tag in post.Tags)
            {
                if (repository.Tags.Any(x => x.Name == tag.Name))
                {
                    repository.Tags.Attach(tag);
                }
            }
        }
    }
}