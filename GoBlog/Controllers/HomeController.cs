using AutoMapper;
using GoBlog.Infrastructure.Paging;
using GoBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GoBlog.Persistence;

namespace GoBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var posts = repository.Posts.OrderBy(post => post.Published);
            var postViewModels = Mapper.Map<List<PostViewModel>>(posts);
            var pagedList = postViewModels.ToPagedList(pageNumber, 2);
            return View("Index", pagedList);
        }

        public ActionResult Post(string slug)
        {
            // Thought: what if the post does not exist.
            var post = repository.Posts.FirstOrDefault(p => p.Slug == slug);
            var postViewModel = Mapper.Map<PostViewModel>(post);
            return View("Post", postViewModel);
        }
    }
}