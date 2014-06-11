using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoBlog.Domain.Infrastructure.Persistence.Entities
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }
        public virtual ICollection<Entry> Entries { get; set; } 
    }
}