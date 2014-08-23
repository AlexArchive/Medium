using System.Linq;
using GoBlog.Common.Pagination;
using GoBlog.Domain;
using System.Web.Mvc;

namespace GoBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsRepository repository;
        private const int PageSize = 5;

        public HomeController()
            : this(new PostsRepository(new DatabaseContext()))
        {

        }

        public HomeController(IPostsRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var posts = 
                repository.All()
                          .Paginate(pageNumber, PageSize);

            if (posts.Any() || pageNumber == 1)
            {
                return View(posts);
            }

            return RedirectToAction("Index", new { pageNumber = posts.TotalPageCount });
        }
    }
}