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
        private readonly IPostsRepository repository;

        public PostsController(IPostsRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index(int pageNumber = 1)
        {
            var posts = 
                repository.All()
                          .Where(post => !post.Draft)
                          .OrderBy(post => post.PublishedAt);

            var model = 
                posts.MapTo<PostViewModel>()
                     .ToPagedList(pageNumber, PageSize);

            return View("Index", model);
        }

        public ActionResult Post(string slug)
        {
            var post =
                repository.Find(slug);

            if (post == null || post.Draft)
            {
                return HttpNotFound();
            }

            return View("Post", post.MapTo<PostViewModel>());
        }
    }
}