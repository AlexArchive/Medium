using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using AutoMapper;
using Blog.Core.Infrastructure.Persistence.Entities;
using Blog.Core.Service;
using Blog.Models;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]
    public class EntriesController : Controller
    {
        private readonly EntryService _entryService = new EntryService();

        public EntriesController()
        {
            ViewBag.EntryCount = _entryService.EntriesCount();
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
                var entity = Mapper.Map<BlogEntry>(model);
                bool success = _entryService.Add(entity);

                if (success)
                {
                    ViewBag.EntryLink = LinkToEntry(entity.Slug);
                    ViewBag.EntryCount = _entryService.EntriesCount(); // for some reason the .ctor doesn't work here.
                    return View();
                }

                ModelState.AddModelError("", "You have previously published a blog entry with this slug. Please choose another one.");
            }

            return View(model);
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

            var input = new EntryInput
            {
                Header = entry.Header,
                Content = entry.Content,
                Published = entry.Published,
                AllowComments = entry.AllowComments
            };

            return View("Add", input);
        }


        [HttpPost]
        public ActionResult Edit(EntryInput input)
        {
            BlogEntry entry = new BlogEntry
            {
                Slug =  input.Header.ToLower().Replace(' ', '-'),
                Header = input.Header,
                Content = input.Content,
                Published = input.Published,
                AllowComments = input.AllowComments
            };

            _entryService.Update(entry);

            ViewBag.EntryLink = LinkToEntry(entry.Slug);

            return View("Add", input);
        }

        private string LinkToEntry(string headerSlug)
        {
            var helper = new UrlHelper(ControllerContext.RequestContext);
            return helper.Action("Entry", "Blog", new { area = "", headerSlug });
        }
    }
}