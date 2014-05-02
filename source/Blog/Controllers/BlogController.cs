using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
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

            var model = Mapper.Map<PagedList<Entry>>(pagedEntries);

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

            var cookie = Request.Cookies["ViewedPage"];
            if (cookie == null)
            {
                cookie = new HttpCookie("ViewedPage");
                cookie.Expires = DateTime.Now.AddHours(1);
            }

            if (cookie["slug_" + entry.HeaderSlug] == null)
            {
                cookie["slug_" + entry.HeaderSlug] = "1";
                Response.Cookies.Add(cookie);

                entry.Views ++;
                _entryService.Update(entry);
                Debug.Print("Entry(): Incrementing view count for {0}", entry.HeaderSlug);
            }
            else
            {
                Debug.Print("Entry(): Already incremented view for {0}. Not doing it again lol.", entry.HeaderSlug);
            }
            var model = Mapper.Map<Entry>(entry);

            return View("Entry", model);
        }

        public ActionResult About()
        {
            return Entry("about-me");
        }
    }
}