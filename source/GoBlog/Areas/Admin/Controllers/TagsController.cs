using System.Web.Mvc;
using GoBlog.Domain.Service;

namespace GoBlog.Areas.Admin.Controllers
{
    public class TagsController : Controller
    {
        private readonly TagsService _tagsService = new TagsService();

        public JsonResult Search(string term)
        {
            var tags = _tagsService.Search(term);
            return Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}