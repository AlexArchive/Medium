using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Medium.WebModel
{
    public static class StringExtensions
    {
        public static string ToSlug(this string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.All(char.IsWhiteSpace))
            {
                throw new ArgumentException("input");
            }

            var slug = input.ToLowerInvariant();
            slug = Regex.Replace(slug, "[\\s_]", "-");
            slug = Regex.Replace(slug, "[^a-z0-9-]", "");
            slug = Regex.Replace(slug, "-+", "-");
            slug = slug.Trim('-');
            return slug;
        }
    }
}