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

        public bool Draft { get; set; }
    }

    public class EntryInput
    {
        [HiddenInput]
        public string Slug { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        public bool Draft { get; set; }

        public bool AllowComments { get; set; }
    }
}