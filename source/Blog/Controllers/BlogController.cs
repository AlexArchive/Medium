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
                return HttpNotFound();
            }
            
            return View("Entry", entry);
        }

        public ActionResult About()
        {
            return Entry("about-me");
        }
    }
}