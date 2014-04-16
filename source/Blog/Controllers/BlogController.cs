using System.Web.Mvc;
using Blog.Core.Service;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private const int ENTRIES_PER_PAGE = 5;

        private readonly BlogService _service = new BlogService();

        public ActionResult Index(int pageNumber = 1)
        {
            var pagedEntries = _service.GetBlogEntries(pageNumber, ENTRIES_PER_PAGE);
            return View(pagedEntries);
        }

        public ActionResult Entry(string headerSlug)
        {
            var entry = _service.GetBlogEntry(headerSlug);
            if (entry == null)
            {
                return HttpNotFound("no blog entry found");
            }
            return View(entry);
        }

        public ActionResult About()
        {
            var entry = _service.GetBlogEntry("about-me");
            if (entry != null)
            {
                return View("Entry", entry);
            }

            return Content("no about-me defined");
        }
    }
}