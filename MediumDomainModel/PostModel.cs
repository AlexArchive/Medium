﻿using System;

namespace Medium.DomainModel
{
    public class PostModel
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}