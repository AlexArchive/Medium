using System.Collections.Generic;

namespace Medium.DomainModel
{
    public class TagsModel
    {
        public IEnumerable<TagModel> Tags { get; set; }
        public Dictionary<TagModel, string[]> TagsMap { get; set; }
    }
}