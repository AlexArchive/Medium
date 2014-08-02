using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoBlog.Data.Entities
{
    public class Post
    {
        [Key]
        [Required]
        public string Slug { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; }

        public bool Draft { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}