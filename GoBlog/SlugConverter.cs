using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace GoBlog
{
    public static class SlugConverter
    {
        public static string Convert(string title)
        {
            title = title.ToLowerInvariant();
            title = Regex.Replace(title, "[^\\w]", "-");
            title = Regex.Replace(title, "-+", "-");
            title = title.Trim('-');
            return title;
        }
    }
}