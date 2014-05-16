using System.Diagnostics;
using AutoMapper;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Core.Service;
using Blog.Infrastructure.AutoMapper;
using Blog.Infrastructure.Common;
using Blog.Models;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]
    public class EntriesController : Controller
    {
        private readonly EntryService _entryService = new EntryService();

        public EntriesController()
        {
            RefreshEntryCount();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(EntryInput model)
        {
            if (ModelState.IsValid)
            {
                var entry = Mapper.Map<BlogEntry>(model);

                var success = _entryService.Add(entry);

                if (success)
                {
                    ViewBag.EntryLink = LinkToEntry(entry.Slug);
                    RefreshEntryCount();
                    return View();
                }
                ModelState.AddModelError("", "You have previously published a blog entry with this slug. Please choose another one.");
            }

            return View(model);
        }

        public ActionResult All()
        {
            var entries = _entryService.List();
            return View(entries);
        }

        public ActionResult Delete(string slug)
        {
            _entryService.Delete(slug);
            return RedirectToAction("All");
        }

        public ActionResult Edit(string slug)
        {
            var entry = _entryService.Single(slug);

            var model = Mapper.Map<EntryInput>(entry);

            return View("Add", model);
        }

        [HttpPost]
        public ActionResult Edit(EntryInput input)
        {
            var entry = _entryService.Single(input.Slug);
            input.MapPropertiesToInstance(entry);
            
            _entryService.Update(entry);
            ViewBag.EntryLink = LinkToEntry(entry.Slug);

            return View("Add", input);
        }

        private string LinkToEntry(string slug)
        {
            var helper = new UrlHelper(ControllerContext.RequestContext);
            return helper.LinkToEntry(slug);
        }

        private void RefreshEntryCount()
        {
            ViewBag.EntryCount = _entryService.EntriesCount();
        }
    }
}