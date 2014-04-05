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

        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
