using System;
using System.Web.Mvc;
using Blog.Core.Service;
using Blog.Models;

namespace Blog.Areas.Admin.Controllers
{
    public class EntriesController : Controller
    {
        private readonly BlogService _service = new BlogService();

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(EntryInput model)
        {
            if (ModelState.IsValid)
            {
                bool success = _service.AddBlogEntry(model.Header, model.HeaderSlug, model.Content);
                if (success)
                {
                    ViewBag.SuccessMessage = "Post Added Successfully";
                    return View();
                }

                ModelState.AddModelError(
                    string.Empty,
                    string.Format("You have previously used the header slug \"{0}\". Please choose another one.", model.HeaderSlug));
            }

            return View(model);
        }
    }
}