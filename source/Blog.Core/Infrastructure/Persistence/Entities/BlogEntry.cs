using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Infrastructure.Persistence.Entities
{
    public class BlogEntry
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

        public bool AllowComments { get; set; }

        public bool Draft { get; set; }

        public int Views { get; set; }

        public bool Published
        {
            get { return !Draft && PublishedAt <= DateTime.Now; }
        }
    }
}