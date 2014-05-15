using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.Models
{
    public class EntryViewModel
    {
        public string Slug { get; set; }

        public string Header { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public DateTime PublishedAt { get; set; }

        public bool AllowComments { get; set; }
    }

    public class EntryInput
    {
        [Required]
        public string Header { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        public bool Published { get; set; }

        public bool AllowComments { get; set; }
    }
}