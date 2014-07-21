using System;

namespace GoBlog.Models
{
    public class PostViewModel
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public bool Draft { get; set; }
    }
}
