using AutoMapper;
using Blog.Core.Paging;
using Blog.Core.Service;
using Blog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly EntryService _entryService = new EntryService();
        private const int EntiresPerPage = 5;
        
        public ActionResult Index(int pageNumber = 1)
        {
            var entries =
                _entryService.PaginatedList(pageNumber, EntiresPerPage).Where(p => p.Published);

            var model = Mapper.Map<PagedList<EntryViewModel>>(entries);

            return View(model);
        }

        public ActionResult Entry(string slug)
        {
            var entry = _entryService.Single(slug);

            if (entry == null || !entry.Published)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<EntryViewModel>(entry);

            return View("Entry", model);
        }

        public ActionResult Archive()
        {
            var entries = 
                _entryService.List().Where(e => e.Published);

            var model = Mapper.Map<List<EntryViewModel>>(entries);

            return View(model);
        }
    }
}