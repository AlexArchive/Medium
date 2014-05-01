using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Data.Entities
{
    public class BlogEntry
    {
        [Key]
        [Required]
        [StringLength(250)]
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

        public int Views { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
