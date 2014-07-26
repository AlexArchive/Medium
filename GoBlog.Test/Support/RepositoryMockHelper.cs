﻿using System.Linq;
using GoBlog.Data;
using GoBlog.Data.Entities;
using Moq;
using System;
using System.Collections.ObjectModel;

namespace GoBlog.Test.Support
{
    internal static class RepositoryMockHelper
    {
        internal static Mock<IRepository> MockRepository()
        {
            var repository = new Mock<IRepository>();

            var posts = new FakeDbSet<Post>((post, key) => post.Slug == (string) key)
            {
                new Post 
                {
                    Draft     = true,
                    Slug      = "a-face-made-for-email-part-three",
                    Title     = "A face made for email, part three",
                    Summary   = "It has happened again: someone",
                    Content   = "It has happened again: someone",
                    PublishedAt = DateTime.Now.AddDays(8),
                    // EF entities will never be equal to null - pretending that they could be will 
                    // require one useless nullity check.
                    Tags = new Collection<Tag>()
                },
                new Post 
                {
                    Slug      = "continuing-to-an-outer-loop",
                    Title     = "Continuing to an outer loop",
                    Summary   = "When you have a nested loop, sometimes",
                    Content   = "When you have a nested loop, sometimes",
                    PublishedAt = DateTime.Now.AddDays(7),
                    Tags = new Collection<Tag> { new Tag { Name="Programming" } }
                },
                new Post 
                {
                    Slug      = "bad-comparisons-part-four",
                    Title     = "Bad comparisons, part four",
                    Summary   = "One more easy one.",
                    Content   = "One more easy one.",
                    PublishedAt = DateTime.Now.AddDays(6),
                    Tags = new Collection<Tag>()
                },
                new Post 
                {
                    Slug      = "alas-smith-and-jones",
                    Title     = "Alas, Smith and Jones",
                    Summary   = "We have a feature in C#",
                    Content   = "We have a feature in C#",
                    PublishedAt = DateTime.Now.AddDays(5),
                    Tags = new Collection<Tag>()
                },
                new Post 
                {
                    Slug      = "defect-spotting-at-the-hub",
                    Title     = "Defect spotting at the HUB",
                    Summary   = "The University of Washington chapter",
                    Content   = "The University of Washington chapter",
                    PublishedAt = DateTime.Now.AddDays(4),
                    Tags = new Collection<Tag>()
                },
                new Post 
                {
                    Slug      = "analyzing-test-code",
                    Title     = "Analyzing test code",
                    Summary   = "Continuing with my series",
                    Content   = "Continuing with my series",
                    PublishedAt = DateTime.Now.AddDays(3),
                    Tags = new Collection<Tag>()
                },
                new Post 
                {
                    Slug      = "when-should-i-write-a-property",
                    Title     = "When should I write a property?",
                    Summary   = "One of the questions I’m asked",
                    Content   = "One of the questions I’m asked",
                    PublishedAt = DateTime.Now.AddDays(2)
                },
                new Post 
                {
                    Slug      = "lowering-in-language-design-part-two",
                    Title     = "Lowering in language design, part two",
                    Summary   = "Last time on FAIC I described how",
                    Content   = "Last time on FAIC I described how",
                    PublishedAt = DateTime.Now.AddDays(3),
                    Tags = new Collection<Tag>()
                },
                new Post 
                {
                    Slug      = "dynamic-contagion-part-one",
                    Title     = "Dynamic contagion, part one",
                    Summary   = "Suppose you're an epidemiologis",
                    Content   = "Suppose you're an epidemiologis",
                    PublishedAt = DateTime.Now,
                    Tags = new Collection<Tag>()
                },
                new Post 
                {
                    Slug      = "dynamic-contagion-part-two",
                    Title     = "Dynamic contagion, part two",
                    Summary   = "Last time I discussed how",
                    Content   = "Last time I discussed how",
                    PublishedAt = DateTime.Now.AddDays(1),
                    Tags = new Collection<Tag>()
                }
            };
            repository.Setup(db => db.Posts).Returns(posts);

            var tags = new FakeDbSet<Tag>((tag, key) => tag.Name == (string) key);
            repository.Setup(db => db.Tags).Returns(tags);

            return repository;
        }
    }
}