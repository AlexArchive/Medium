using System.Collections.Generic;
using AutoMapper;
using Blog.Core.Paging;
using Blog.Core.Service;
using Blog.Models;
using System;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _entryService = new BlogService();
        private const int ENTRIES_PER_PAGE = 5;

        public ActionResult Index(int pageNumber = 1)
        {
            var pagedEntries = 
                _entryService.GetBlogEntries(pageNumber, ENTRIES_PER_PAGE);

            var model = Mapper.Map<PagedList<EntryViewModel>>(pagedEntries);

            return View(model);
        }

        public ActionResult Entry(string headerSlug)
        {
            if (headerSlug == null)
                throw new ArgumentNullException("headerSlug");

            var entry = _entryService.Get(headerSlug);
            if (entry == null) 
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<EntryViewModel>(entry);

            // need to specify the view name explicitly so that other
            // actions can present blog entries using this action.
            return View("Entry", model);
        }

        public ActionResult About()
        {
            return Entry("about-me");
        }
    }
}