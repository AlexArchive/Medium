using System;
using System.IO;
using System.Web.Mvc;

namespace Medium.WebModel
{
    public static class TwitterBootstrapMvc
    {
        public static IDisposable Alert(this HtmlHelper helper, string criticalness)
        {
            var element = new TagBuilder("div");
            element.AddCssClass("alert");
            element.AddCssClass(criticalness);
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
}