using GoBlog.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog
{
    public static class TagsResolver
    {
        private const string Delimiter = ",";

        public static IEnumerable<Tag> ResolveFromString(string delimitedTags)
        {
            var tagNames = 
                delimitedTags.Replace(" ", "")
                             .Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries)
                             .Distinct();

            return tagNames.Select(name => new Tag { Name = name.CapitalizeFirstLetter() });
        }

        public static string ResolveFromCollection(IEnumerable<Tag> tags)
        {
            return string.Join(Delimiter + " ", tags.Select(tag => tag.Name));
        }
    }
}