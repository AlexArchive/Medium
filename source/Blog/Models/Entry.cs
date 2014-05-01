using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.Models
{
    public class Entry
    {
        [Display(Name="Slug")]
        public string HeaderSlug { get; set; }
        public string Header { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public int Views { get; set; }
        [Display(Name = "Published")]
        public DateTime PublishDate { get; set; }
    }

    public class EntryInput
    {
        [Required]
        public string Header { get; set; }

        [Required]
        [Display(Name="Slug")]
        [RegularExpression("[a-zA-Z0-9-]+", ErrorMessage = "Slugs can only contain the alphanumeric characters and hyphens.")]
        public string HeaderSlug { get; set; }
        
        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}