using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GoBlog.Data.Entities;

namespace GoBlog.Common
{
    public static class TagsResolver
    {
        private const string Delimiter = ",";

        public static IEnumerable<Tag> ResolveFromString(string delimitedTags)
        {
            var tags = 
                Regex.Replace(delimitedTags, @"(\s*)(,|^|$)(\s*)", "$2")
                     .Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries)
                     .Distinct()
                     .Select(name => new Tag { Name = name.CapitalizeFirstLetter() });
            
            return tags;
        }

        public static string ResolveFromCollection(IEnumerable<Tag> tags)
        {
            return string.Join(Delimiter + " ", tags.Select(tag => tag.Name));
        }
    }
}