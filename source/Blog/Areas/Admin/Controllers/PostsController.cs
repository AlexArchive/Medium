using System.Web.Mvc;
using Blog.Core.Service;
using Blog.Models;

namespace Blog.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogService _service = new BlogService();

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(EntryInput input)
        {
            if (ModelState.IsValid)
            {
                bool success = _service.AddBlogEntry(input.Header, input.HeaderSlug, input.Content);
                if (success)
                {
                    // TODO: Return to Add View with success message instead.
                    return RedirectToAction("Index", "Blog", new { area = "" });
                }

                ModelState.AddModelError(string.Empty, "something went wrong andre");
            }

            return View(input);
        }
    }
}