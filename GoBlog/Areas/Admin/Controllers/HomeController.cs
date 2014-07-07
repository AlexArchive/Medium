using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GoBlog.Models;
using GoBlog.Persistence;

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
            var post = repository.Posts.FirstOrDefault(p => p.Slug == slug);
            repository.Posts.Remove(post);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}