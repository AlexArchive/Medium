using GoBlog.AutoMapper;
using GoBlog.Data;
using GoBlog.Models;
using GoBlog.Paging;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Controllers
{
    public class PostsController : Controller
    {
        private const int PageSize = 8;
        private readonly IRepository repository;

        public PostsController(IRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var posts = 
                repository.Posts
                          .Where(post => !post.Draft)
                          .OrderBy(post => post.PublishedAt);

            var model = 
                posts.MapTo<PostViewModel>()
                     .ToPagedList(pageNumber, PageSize);

            return View(model);
        }

        public ActionResult Post(string slug)
        {
            var post = 
                repository.Posts
                          .FirstOrDefault(p => p.Slug == slug);

            if (post == null || post.Draft)
            {
                return HttpNotFound();
            }

            return View(post.MapTo<PostViewModel>());
        }
    }
}