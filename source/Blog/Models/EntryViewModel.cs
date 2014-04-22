using System;
using System.Collections.Generic;
using Blog.Core.Data.Entities;

namespace Blog.Models
{
    public class EntryViewModel
    {
        public string HeaderSlug { get; set; }

        public string Header { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public int Views { get; set; }

        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }

        //public virtual ICollection<Tag> Tags { get; set; }
    }
}