
using System;

namespace MediumDomainModel
{
    public class PostModel
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }
    }
}