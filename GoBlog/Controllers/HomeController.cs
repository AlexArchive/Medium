using AutoMapper;
using GoBlog.Infrastructure.Paging;
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

        public ActionResult Index(int pageNumber = 1)
        {
            var posts = Mapper.Map<List<PostViewModel>>(repository.Posts.OrderBy(x=> x.Published).ToList());
            var model = new PagedList<PostViewModel>(posts, pageNumber, 2);
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