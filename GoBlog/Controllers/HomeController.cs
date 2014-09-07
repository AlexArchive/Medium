using GoBlog.Common.Pagination;
using GoBlog.Domain;
using System.Linq;
using System.Web.Mvc;

namespace GoBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsRepository repository;
        public const int PageSize = 5;

        public HomeController(IPostsRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var posts = repository.AllPosts().Paginate(pageNumber, PageSize);

            if (posts.Any() || pageNumber == 1)
            {
                return View(posts);
            }

            return RedirectToAction("Index", new { pageNumber = posts.TotalPageCount });
        }

        public ActionResult Post(string slug)
        {
            var post = repository.FindPost(slug);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

    }
}