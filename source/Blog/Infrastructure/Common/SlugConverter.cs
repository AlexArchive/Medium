using System.Text.RegularExpressions;
using System.Web;

namespace Blog.Infrastructure.Common
{
    public class SlugConverter
    {
        public static string Convert(string title)
        {
            title = HttpUtility.HtmlDecode(title);

            title = title.ToLowerInvariant();

            title = Regex.Replace(title, "\\s+", "-");

            title = title.Trim('-');

            return title;
        }
    }
}