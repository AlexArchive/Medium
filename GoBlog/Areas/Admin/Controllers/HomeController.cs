using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            : this (new BlogDatabase())
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
            // Thought: What if we are supplied a slug that does not exist? Test this!
            var post = repository.Posts.FirstOrDefault(p => p.Slug == slug);
            repository.Posts.Remove(post);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string slug)
        {
            var post = repository.Posts.FirstOrDefault(p => p.Slug == slug);
            var postModel = Mapper.Map<PostInputModel>(post);
            return View("Edit", postModel);
        }

        [HttpPost]
        public ActionResult Edit(PostInputModel model)
        {
            if (ModelState.IsValid)
            {
                // Thought: What if we are supplied a slug that does not exist? Test this!
                Post existing = repository.Posts.FirstOrDefault(post => post.Slug == model.Slug);
                Mapper.Map(model, existing, model.GetType(), typeof (Post));
                repository.SaveChanges();
                return Edit(existing.Slug);
            }

            return View("Edit", model);
        }
    }
}