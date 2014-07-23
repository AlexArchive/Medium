using GoBlog.Persistence.Entities;
using NUnit.Framework;
using System.Linq;

namespace GoBlog.Test
{
    [TestFixture]
    public class TagsResolverTest
    {
        // Feature: Replaces spaces with hyphen (unit testing -> unit-testing).
        
        [Test]
        public void ResolveFromString_AppliesTitleCasing()
        {
            var actual = TagsResolver.ResolveFromString("programming, general");

            var expected = new[] { "Programming", "General" };
            CollectionAssert.AreEquivalent(expected, actual.Select(x => x.Name));
        }

        [Test]
        public void ResolveFromString_RemovesDuplicateTags()
        {
            var actual = TagsResolver.ResolveFromString("programming, programming");

            var expected = new[] { "Programming" };
            CollectionAssert.AreEquivalent(expected, actual.Select(x => x.Name));
        }

        [Test]
        public void ResolveFromString_RemovesEmptyTags()
        {
            var actual = TagsResolver.ResolveFromString(",programming, ,,");

            var expected = new[] { "Programming" };
            CollectionAssert.AreEquivalent(expected, actual.Select(x => x.Name));
        }

        [Test]
        public void ResolveFromString_RemovesSpaces()
        {
            var actual = TagsResolver.ResolveFromString(" programming  , general   ");

            var expected = new[] { "Programming", "General" };
            CollectionAssert.AreEquivalent(expected, actual.Select(x => x.Name));
        }

        [Test]
        public void ResolveFromString_RemovesExtraneousSpaces()
        {
            var actual = TagsResolver.ResolveFromString("programming. general.");

            var expected = new[] { "Programming. general." };
            CollectionAssert.AreEquivalent(expected, actual.Select(x => x.Name));
        }

        [Test]
        public void ResolveFromSequence_ReturnsExpected()
        {
            var collection = new[] 
            {
                new Tag { Name = "General" },
                new Tag { Name = "Programming" } 
            };

            var actual = TagsResolver.ResolveFromCollection(collection);

            Assert.That(actual, Is.EqualTo("General, Programming"));
        }
    }
}