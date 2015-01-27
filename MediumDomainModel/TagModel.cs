using System;

namespace Medium.DomainModel
{
    public class TagModel : IEquatable<TagModel>
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public bool Equals(TagModel other)
        {
            return other.Name == Name;
        }

        public override bool Equals(object other)
        {
            return Equals((TagModel) other);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * 17 * Count.GetHashCode();
        }
    }
}