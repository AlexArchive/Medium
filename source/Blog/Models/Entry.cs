using System.Text.RegularExpressions;
using Blog.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.Models
{
    public class Entry
    {
        public string HeaderSlug { get; set; }
        public string Header { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public int Views { get; set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }

    public class EntryInput
    {
        [Required]
        public string Header { get; set; }

        [Required]
        [Display(Name="Header Slug")]
        [RegularExpression("[a-zA-Z0-9-]+", ErrorMessage = "Slugs can only contain the alphanumeric characters and hyphens.")]
        public string HeaderSlug { get; set; }
        
        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }


}