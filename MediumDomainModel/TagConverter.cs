using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Medium.DomainModel
{
    public class TagConverter
    {
        const string Delimiter = ",";

        public static IEnumerable<string> Convert(string delimitedTags)
        {
            return Regex
                .Replace(delimitedTags, @"(\s*)(,|^|$)(\s*)", "$2")
                .Trim()
                .Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct();
        }

        public static string Convert(IEnumerable<string> tags)
        {
            return string.Join(Delimiter + " ", tags);
        }
    }
}