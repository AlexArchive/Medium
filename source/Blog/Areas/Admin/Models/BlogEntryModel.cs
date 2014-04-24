using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Models
{
    public class BlogEntryModel
    {
        [Required]
        public string Header { get; set; }

        [Required]
        public string HeaderSlug { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}