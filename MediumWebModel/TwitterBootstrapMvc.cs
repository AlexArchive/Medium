using System;
using System.IO;
using System.Web.Mvc;

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
}