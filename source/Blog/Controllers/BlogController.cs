﻿using System.Web.Mvc;
using Blog.Core.Service;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private const int ENTRIES_PER_PAGE = 6;

        private readonly BlogService _service = new BlogService();

        public ActionResult Index(int pageNumber = 1)
        {
            var entries = _service.GetBlogEntries(pageNumber, ENTRIES_PER_PAGE);
            return View(entries);
        }
    }
}