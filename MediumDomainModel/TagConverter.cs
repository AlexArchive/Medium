using System;
using System.Collections.Generic;
using System.Linq;

namespace Medium.DomainModel
{
    public class TagConverter
    {
        const string Delimiter = ",";

        public static IEnumerable<string> Convert(string delimitedTags)
        {
            return delimitedTags
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