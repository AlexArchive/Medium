using System.Web.Mvc;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Core.Service;
using Blog.Models;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]
    public class EntriesController : Controller
    {
        private readonly EntryService _entryService = new EntryService();

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(EntryInput model)
        {
            if (ModelState.IsValid)
            {
                bool success = _entryService.Add(model.Header, model.HeaderSlug, model.Content);
                if (success)
                {
                    var entryLink = LinkToEntry(model.HeaderSlug);
                    return Json("Entry Published. You can view the entry here: " + entryLink);
                }

                return Json("You have previously published a blog entry with this slug. Please choose another one.");
            }

            return Json("Ensure that your input is valid.");
        }

        private string LinkToEntry(string headerSlug)
        {
            var helper = new UrlHelper(ControllerContext.RequestContext);
            return helper.Action("Entry", "Blog", new { area = "", headerSlug = headerSlug });
        }


        public ActionResult All()
        {
            var pagedEntries =
                _entryService.List();

            return View(pagedEntries);
        }

        public ActionResult Delete(string slug)
        {
            _entryService.Delete(slug);
            return RedirectToAction("All");
        }


        public ActionResult Edit(string slug)
        {
            var entry = _entryService.Get(slug);
            

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

           _entryService.Update(entry);

           var helper = new UrlHelper(ControllerContext.RequestContext);
           var linkToEntry = helper.Action("Entry", "Blog", new { area = "", headerSlug = entry.HeaderSlug });
           ViewBag.LinkToEntry = linkToEntry;

            return View("Add", input);
        }



    }
}