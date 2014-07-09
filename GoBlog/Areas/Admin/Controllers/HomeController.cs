using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using GoBlog.Areas.Admin.Models;
using GoBlog.Models;
using GoBlog.Persistence;
using GoBlog.Persistence.Entities;

namespace GoBlog.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController()
            : this(new BlogDatabase())
        {

        }

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            var posts = repository.Posts.OrderBy(post => post.Published);
            var postModels = Mapper.Map<List<PostViewModel>>(posts);
            return View("Index", postModels);
        }

        public ActionResult Delete(string slug)
        {
            var post = repository.Posts.FirstOrDefault(p => p.Slug == slug);
            if (post == null) return HttpNotFound("You cannot delete a post that does not exist.");
            repository.Posts.Remove(post);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string slug)
        {
            var post = repository.Posts.FirstOrDefault(p => p.Slug == slug);
            if (post == null) return HttpNotFound("You cannot edit a post that does not exist.");
            var postModel = Mapper.Map<PostInputModel>(post);
            return View("Edit", postModel);
        }

        [HttpPost]
        public ActionResult Edit(PostInputModel model)
        {
            if (ModelState.IsValid)
            {
                Post existing = repository.Posts.FirstOrDefault(post => post.Slug == model.Slug);
                repository.Posts.Remove(existing);
                repository.SaveChanges();
                if (existing == null) return HttpNotFound("You cannot edit a post that does not exist.");
                Mapper.Map(model, existing, model.GetType(), typeof(Post));
                existing.Summary = Summarize(existing.Content);
                existing.Slug = SlugConverter.Convert(existing.Title);
                repository.Posts.Add(existing);
                repository.SaveChanges();
                return Edit(existing.Slug);
            }

            return View("Edit", model);
        }

        private static string Summarize(string content)
        {
            if (!content.Contains(Environment.NewLine)) return content;
            return content.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];
        }
    }
}