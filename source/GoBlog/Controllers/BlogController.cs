using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GoBlog.Domain.Paging;
using GoBlog.Domain.Service;
using GoBlog.Models;

namespace GoBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly EntryService _entryService = new EntryService();
        private const int EntiresPerPage = 5;
        
        public ActionResult Index(int pageNumber = 1)
        {
            var entries =
                _entryService.PaginatedList(pageNumber, EntiresPerPage);

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