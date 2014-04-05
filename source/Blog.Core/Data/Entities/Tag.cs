using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Data.Entities
{
    public class Tag
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<BlogEntry> Entries { get; set; }
    }
}