using GoBlog.Domain.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.Infrastructure.AutoMapper.Resolvers
{
    public class TagsResolver
    {
        const string Delimiter = ",";

        public static string ResolveTags(IEnumerable<Tag> tags)
        {
            return string.Join(Delimiter + " ", tags.Select(x => x.Name));
        }

        public static IEnumerable<Tag> ResolveTags(string delimitedTags)
        {
            var tags = delimitedTags.Trim().Split(new[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries);
            return tags.Select(x => new Tag { Name = x });
        }
    }
}