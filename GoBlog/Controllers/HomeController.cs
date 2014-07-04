using AutoMapper;
using GoBlog.Infrastructure.Persistence;
using GoBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;

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
            var posts = repository.Posts.ToList();
            var model = Mapper.Map<List<PostViewModel>>(posts);
            return View("Index", model);
        }

        public ActionResult Post(string slug)
        {
            var post  = repository.Posts.FirstOrDefault(p => p.Slug == slug);
            var model = Mapper.Map<PostViewModel>(post);
            return View("Post", model);
        }
    }
}