using System.Text.RegularExpressions;
using System.Web;

namespace GoBlog.Infrastructure.Common
{
    public class SlugConverter
    {
        public static string Convert(string title)
        {
            title = HttpUtility.HtmlDecode(title);

            title = title.ToLowerInvariant();

            title = Regex.Replace(title, "\\s+", "-");

            title = Regex.Replace(title, "[^\\w\\s]", "-");

            title = title.Trim('-');

            return title;
        }
    }
}