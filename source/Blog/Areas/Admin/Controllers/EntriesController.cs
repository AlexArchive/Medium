using System;
using System.Threading;
using System.Web.Mvc;
using Blog.Core.Data.Entities;
using Blog.Core.Service;
using Blog.Models;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]

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
                    var helper = new UrlHelper(ControllerContext.RequestContext);
                    var linkToEntry = helper.Action("Entry", "Blog", new { area = "", headerSlug = model.HeaderSlug});
                    ViewBag.LinkToEntry = linkToEntry;

                    return View();
                }

                ModelState.AddModelError(
                    string.Empty,
                    string.Format("You have previously used the header slug \"{0}\". Please choose another one.", model.HeaderSlug));
            }

            return View(model);
        }

        public ActionResult All()
        {
            var pagedEntries =
                _service.GetAll();

            return View(pagedEntries);
        }

        public ActionResult Delete(string slug)
        {
            _service.Delete(slug);
            return RedirectToAction("All");
        }


        public ActionResult Edit(string slug)
        {
            var entry = _service.Get(slug);
            

            EntryInput input = new EntryInput
            {
                HeaderSlug = entry.HeaderSlug,
                Header = entry.Header,
                Content = entry.Content
            };

            return View("Add", input);
        }


        [HttpPost]
        public ActionResult Edit(EntryInput input)
        {
            BlogEntry entry = new BlogEntry
            {
                HeaderSlug = input.HeaderSlug,
                Header = input.Header,
                Content = input.Content
            };

           _service.Update(entry);

           var helper = new UrlHelper(ControllerContext.RequestContext);
           var linkToEntry = helper.Action("Entry", "Blog", new { area = "", headerSlug = entry.HeaderSlug });
           ViewBag.LinkToEntry = linkToEntry;

            return View("Add", input);
        }



    }
}