using System.Web.Mvc;

namespace Medium.WebModel
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid)
            {
                return;
            }

            filterContext.Result = new ViewResult
            {
                ViewName = filterContext.ActionDescriptor.ActionName,
                ViewData = filterContext.Controller.ViewData,
                TempData = filterContext.Controller.TempData
            };
        }
    }

}