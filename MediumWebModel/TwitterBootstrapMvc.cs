using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace Medium.WebModel
{
    public static class TwitterBootstrapMvc
    {
        public static IDisposable Alert(this HtmlHelper helper, AlertCriticalness criticalness)
        {
            var element = new TagBuilder("div");
            element.AddCssClass("alert");
            element.AddCssClass(criticalness.ToString());
            element.Attributes.Add("role", "alert");

            helper.ViewContext
                .Writer
                .Write(element.ToString(TagRenderMode.StartTag));

            return new BootstrapAlert(helper.ViewContext);
        }
    }

    public class BootstrapAlert : IDisposable
    {
        private readonly TextWriter writer;

        public BootstrapAlert(ViewContext context)
        {
            writer = context.Writer;

            var element = new TagBuilder("button");
            element.Attributes.Add("type", "button");
            element.Attributes.Add("data-dismiss", "alert");
            element.AddCssClass("close");
            element.InnerHtml = "&times;";

            writer.Write(element.ToString());
        }

        public void Dispose()
        {
            writer.Write("</div>");
        }
    }

    public sealed class AlertCriticalness
    {
        public static readonly AlertCriticalness Success = new AlertCriticalness("alert-success");
        public static readonly AlertCriticalness Danger = new AlertCriticalness("alert-danger");

        private readonly string criticalness;

        private AlertCriticalness(string criticalness)
        {
            this.criticalness = criticalness;
        }

        public override String ToString()
        {
            return criticalness;
        }
    }

    public static class TwitterNavigation
    {
        public static HtmlString NavigationLi(
           this HtmlHelper helper,
           string linkText,
           string actionName,
           string controllerName)
        {
            const string activeCssClass = "active";
            var currentController = (string)helper.ViewContext.RouteData.Values["controller"];
            var active = string.Equals(currentController, controllerName, StringComparison.CurrentCultureIgnoreCase);
            var element = new TagBuilder("li");
            if (active)
            {
                element.AddCssClass(activeCssClass);
            }
            element.InnerHtml = helper
                .ActionLink(
                    linkText,
                    actionName,
                    controllerName)
                .ToString();
            return new MvcHtmlString(element.ToString());
        }
    }
}