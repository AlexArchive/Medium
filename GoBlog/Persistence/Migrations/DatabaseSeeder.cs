using System;
using System.Data.Entity;
using GoBlog.Persistence.Entities;

namespace GoBlog.Persistence.Migrations
{
    public class DatabaseSeeder : DropCreateDatabaseAlways<BlogDatabase>
    {
        protected override void Seed(BlogDatabase context)
        {
            // arbitrary posts sourced from http://ericlippert.com/

            context.Posts.Add(new Post
            {
                PublishedAt = DateTime.Now.AddDays(1),
                Slug      = "how-to-debug-small-problems", 
                Title     = "How to debug small programs", 
                Summary   = "If you’re reading this, odds are good it’s because I or someone else linked here",
                Content   = @"If you’re reading this, odds are good it’s because I or someone else linked here 
                              from your StackOverflow question shortly before it was closed and deleted. (If 
                              you’re reading this and you’re not in that position, consider leaving your 
                              favourite tips for debugging small programs in the comments.)"
            });

            context.Posts.Add(new Post
            {
                PublishedAt = DateTime.Now.AddDays(2),
                Slug      = "living-with-unchecked-exceptions",
                Title     = "Living with unchecked exceptions",
                Summary   = "Hey there fabulous readers: I’d like to get your opinions on unchecked.",
                Content   = @"Hey there fabulous readers: I’d like to get your opinions on unchecked 
                              exceptions. Before you run off down to the comments and start typing a short 
                              novel, I want to make sure that we’re all on the same page here."
            });

            context.Posts.Add(new Post
            {
                PublishedAt = DateTime.Now.AddDays(3),
                Slug      = "enumerator-bounds",
                Title     = "ATBG: Why do enumerators avoid a bounds check?",
                Summary   = "I am back from a week in which I visited England, Holland, Belgium, France and",
                Content   = @"I am back from a week in which I visited England, Holland, Belgium, France and 
                              Hungary; this is by far the most countries I’ve been to in so little time. It
                              was great to meet so many customers; more on bicycling"
            });

            context.Posts.Add(new Post
            {
                PublishedAt = DateTime.Now.AddDays(4),
                Slug      = "when-should-i-write-a-property",
                Title     = "When should I write a property?",
                Summary   = "One of the questions I’m asked frequently regarding design of C# classes is",
                Content   = @"One of the questions I’m asked frequently regarding design of C# classes 
                              is ""should this be a property or a method?"" Here are my guidelines:"
            });

            context.Posts.Add(new Post
            {
                PublishedAt = DateTime.Now.AddDays(5),
                Slug      = "what-are-the-fundamental-rules-of-pointers",
                Title     = "What are the fundamental rules of pointers?",
                Summary   = "A lot of questions I see in the C tag on StackOverflow are from beginners who have",
                Content   = @"A lot of questions I see in the C tag on StackOverflow are from beginners who 
                              have never been taught the fundamental rules of pointers."
            });

            context.Posts.Add(new Post
            {
                PublishedAt = DateTime.Now.AddDays(6),
                Slug      = "heartbleed-and-static-analysis",
                Title     = "Heartbleed and static analysis",
                Summary   = "In the wake of the security disaster that is the Heartbleed vulnerability",
                Content   = @"In the wake of the security disaster that is the Heartbleed vulnerability, a 
                              number of people have asked me if Coverity’s static analyzer detects defects 
                              like this."
            });

            context.SaveChanges();
        }
    }
}