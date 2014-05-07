using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Infrastructure.Persistence.Entities
{
    public class BlogEntry
    {
        [Key]
        [Required]
        [StringLength(250)]
        [Display(Name = "Slug")]

        /* could we use the fluent API to provide a default value? */
        public string HeaderSlug { get; set; }

        [Required]
        [StringLength(200)]
        public string Header { get; set; }

        [Required]
        [StringLength(2000)]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        [Display(Name = "Approximate Views")]
        public int Views { get; set; }

        [Display(Name = "Published")]
        public DateTime PublishDate { get; set; }
    }
}
