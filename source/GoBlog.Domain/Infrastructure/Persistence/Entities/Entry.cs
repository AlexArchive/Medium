using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoBlog.Domain.Infrastructure.Persistence.Entities
{
    public class Entry
    {
        [Key]
        [Required]
        [StringLength(250)]
        public string Slug { get; set; }

        [Required]
        [StringLength(200)]
        public string Header { get; set; }

        [Required]
        [StringLength(2000)]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; }

        public bool Published { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } 
    }
}