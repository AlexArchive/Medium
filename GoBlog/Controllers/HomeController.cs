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
        private const int PageSize = 8;
        private readonly IRepository repository;

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var posts = repository.Posts.Where(post=> !post.Draft).OrderBy(post => post.PublishedAt);
            var postViewModels = Mapper.Map<List<PostViewModel>>(posts);
            var pagedList = postViewModels.ToPagedList(pageNumber, PageSize);
            return View("Index", pagedList);
        }

        public ActionResult Post(string slug)
        {
            // Thought: what if the post does not exist.
            var post = repository.Posts.FirstOrDefault(p => p.Slug == slug);
            if (post.Draft)
            {
                return HttpNotFound("You cannot view this post because it is a draft.");           
            }
            var postViewModel = Mapper.Map<PostViewModel>(post);
            return View("Post", postViewModel);
        }
    }
}